using BE_102024.DataAces.NetCore.CheckConditions;
using BE_102024.DataAces.NetCore.DAL;
using BE_102024.DataAces.NetCore.DataOpject;
using BE_102024.DataAces.NetCore.DataOpject.RequestData;
using BE_102024.DataAces.NetCore.DBContext;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BE_102024.DataAces.NetCore.DALImpliment
{
    public class AccountRepository : IAccountRepository
    {
        BE_102024Context _context;
        
        public AccountRepository(BE_102024Context context)
        {
            _context = context;
        }
        public async Task<User> UserLogin(AccountLoginRequestData requestData)
        {
            try
            {
                var passWordHash = Security.EncryptPassWord(requestData.Password);

                var user = _context.User.ToList().Where( s => s.UserName == requestData.UserName 
                && s.PassWord == passWordHash && s.Roles == requestData.Roles).FirstOrDefault();

                return user;
            }
            catch (Exception ex) 
            {
                throw;
            }
        }
    }
}
