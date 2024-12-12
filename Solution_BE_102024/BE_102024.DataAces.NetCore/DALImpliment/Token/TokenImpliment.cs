using BE_102024.DataAces.NetCore.DALImpliment.Token;
using BE_102024.DataAces.NetCore.DBContext;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BE_102024.DataAces.NetCore.DALImpliment.ImplimentCreateToken
{
    public class TokenImpliment : IToken
    {
        private IConfiguration _configuration;
        private readonly BE_102024Context _context;
        public TokenImpliment(IConfiguration configuration, BE_102024Context context)
        {
            _configuration = configuration;
            _context = context;
        }

  

        public async Task<JwtSecurityToken> CreateToken(List<Claim> authClaims)
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

        public async Task<int> UserUpdate_RefeshToken(int UserID, string RefeshToken, DateTime RefeshTokenExpiryTime)
        {
            var user = _context.User.Where(s => s.UserID == UserID).FirstOrDefault();
            if (user == null) 
            {
                return -1;
            }
            user.RefeshToken = RefeshToken;
            user.TokenExprired = RefeshTokenExpiryTime;
            _context.User.Update(user);
            _context.SaveChanges();
            return 1;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
