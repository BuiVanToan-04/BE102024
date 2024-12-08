using BE_102024.DataAces.NetCore.CheckConditions.Response;
using BE_102024.DataAces.NetCore.DAL;
using BE_102024.DataAces.NetCore.DataOpject.RequestData;
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
        private IConfiguration _configuration;
        public AuthenticationController(IAccountRepository accountRepository, IConfiguration configuration)
        {
            _accountRepository = accountRepository;
            _configuration = configuration;
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
                    new Claim(ClaimTypes.Role, user.Roles),
                    new Claim(JwtRegisteredClaimNames.NameId, user.UserID.ToString())
                };

                var newToken = CreateToken(authClaims);
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

        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            if (authClaims == null)
            {
                throw new ArgumentNullException(nameof(authClaims), "authClaims cannot be null");
            }
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
