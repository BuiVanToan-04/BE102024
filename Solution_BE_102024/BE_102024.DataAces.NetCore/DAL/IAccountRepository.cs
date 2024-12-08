using BE_102024.DataAces.NetCore.DataOpject;
using BE_102024.DataAces.NetCore.DataOpject.RequestData;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BE_102024.DataAces.NetCore.DAL
{
    public interface IAccountRepository
    {
        Task<User> UserLogin(AccountLoginRequestData requestData);
    }
}
