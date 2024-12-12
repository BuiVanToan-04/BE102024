using BE_102024.DataAces.NetCore.CheckConditions.Response;
using BE_102024.DataAces.NetCore.DAL;
using BE_102024.DataAces.NetCore.DALImpliment.Token;
using BE_102024.DataAces.NetCore.DataOpject.RequestData;
using BE_102024.DataAces.NetCore.DataOpject.TokenModel;
using BE_102024.DataAces.NetCore.DBContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BE_10_2024_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IAccountRepository _accountRepository;
        private IToken _createToken;
        private IConfiguration _configuration;
        private BE_102024Context _context;
        public AuthenticationController(IAccountRepository accountRepository, IToken createToken, 
            IConfiguration configuration, BE_102024Context context)
        {
            _accountRepository = accountRepository;
            _createToken = createToken;
            _configuration = configuration;
            _context = context;
        }
        [HttpPost("Login_Account")]
        public async Task<IActionResult> Login_Account(AccountLoginRequestData requestData)
        {
            var responseData = new UserLoginResponseData();
            try
            {
                //Bưới 1: Đăng nhập
                var user = await _accountRepository.UserLogin(requestData);
                if (user == null) 
                {
                    responseData.ResponseCode = -1;
                    responseData.ResposeMessage = "Đăng nhập thất bại";
                    return Ok(responseData);
                }

                //Bước 2: Tạo Token
                var authClaims = new List<Claim> { 
                    new Claim(ClaimTypes.Name, user.UserName), 
                    new Claim(ClaimTypes.PrimarySid, user.UserID.ToString())
                };

                var newToken = await _createToken.CreateToken(authClaims);

                //Bước 3: Lưu RefeshToken
                _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);
                var refeshToken = _createToken.GenerateRefreshToken();
                _createToken.UserUpdate_RefeshToken(user.UserID, refeshToken, DateTime.Now.AddDays(refreshTokenValidityInDays));

                //Bước 4: Trả về Token
                responseData.ResponseCode = 1;
                responseData.ResposeMessage = "Đăng nhập thành công";
                responseData.Token = new JwtSecurityTokenHandler().WriteToken(newToken);
                return Ok(responseData);
            }
            catch (Exception ex)
            {
                responseData.ResponseCode = -99;
                responseData.ResposeMessage = ex.Message;
                return Ok(responseData);
            }
        }



        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenModel tokenModel)
        {
            try
            {
                if (tokenModel == null)
                {
                    return BadRequest("Yêu cầu Token không hợp lệ");
                }
                string? accessToken = tokenModel.AccessToken;
                string? refreshToken = tokenModel.RefreshToken;

                //Bước 1: Giai mã Token truyền lên để lấy claims
                var principal = GetPrincipalFromExpiredToken(tokenModel.AccessToken);
                if (principal == null)
                {
                    return Ok("Không hợp lệ");
                }

                //Bước 2: Check RefresToken và ngày hết hạn
                string userName = principal.Identity.Name;

                //Gọi db để lấy theo userName
                var user = _context.User.ToList().Where(s=>s.UserName == userName).FirstOrDefault();

                //Nếu ngày hết hạn < thời gian hiện tại || RefreshToken truyền lên khác RefreshToken
                if (user == null || user.RefeshToken != refreshToken || user.TokenExprired <= DateTime.Now)
                {
                    return BadRequest("Token không hợp lệ || Hết thời gian sử dụng");
                }
                //Bước 3: Tạo Token mới và RefresToken mới
                var authClaims = new List<Claim> {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.PrimarySid, user.UserID.ToString())
                };
                var newToken = await _createToken.CreateToken(authClaims);

                //Tạo RefreshToken mới
                _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);
                var refeshToken = _createToken.GenerateRefreshToken();
                _createToken.UserUpdate_RefeshToken(user.UserID, refeshToken, DateTime.Now.AddDays(refreshTokenValidityInDays));
                return Ok();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex);
            }
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token) //Hàm giải mã token truyền lên
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
    }
}
