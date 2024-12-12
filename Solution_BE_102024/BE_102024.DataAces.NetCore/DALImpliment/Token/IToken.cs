using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BE_102024.DataAces.NetCore.DALImpliment.Token
{
    public interface IToken
    {
        public Task<JwtSecurityToken> CreateToken(List<Claim> authClaims);

        Task<int> UserUpdate_RefeshToken(int UserID, string RefeshToken, DateTime RefeshTokenExpiryTime);
        string GenerateRefreshToken();
    }
}
