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
        private IConfiguration _configuration;

        public AccountRepository(BE_102024Context context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<Function> GetFunctionIDByName(string functionCode)
        {
            return _context.Function.Where(s=> s.FunctionCode == functionCode).FirstOrDefault();
        }

        public async Task<Permission> GetPermisstionUserIDOfFunctionID(int UserID, int functionID)
        {
            return _context.Permission.Where(s=> s.UserID == UserID && s.FunctionID == functionID).FirstOrDefault();
        }

        public async Task<User> UserLogin(AccountLoginRequestData requestData)
        {
            try
            {
                var passWordHash = Security.EncryptPassWord(requestData.Password);

                var user = _context.User.ToList().Where( s => s.UserName == requestData.UserName 
                && s.PassWord == passWordHash).FirstOrDefault();

                return user;
            }
            catch (Exception ex) 
            {
                throw;
            }
        }
        
    }
}
