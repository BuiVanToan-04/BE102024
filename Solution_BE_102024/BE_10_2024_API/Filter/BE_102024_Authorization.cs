using BE_102024.DataAces.NetCore.DAL;
using BE_102024.DataAces.NetCore.DBContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace BE_10_2024_API.Filter
{
    public class BE_102024_Authorization : TypeFilterAttribute
    {
        public BE_102024_Authorization(string functionCode, string permission) : base(typeof(DemoAuthorizeActionFileter))
        {
            Arguments = new object[] { functionCode, permission }; 
        }
    }
    public class DemoAuthorizeActionFileter : IAsyncAuthorizationFilter
    {
        private readonly string _functionCode;
        private readonly string _permission;
        private readonly IAccountRepository _accountRepository;
        public DemoAuthorizeActionFileter(string functionCode, string permission, IAccountRepository accountRepository)
        {
            _functionCode = functionCode;
            _permission = permission;
            _accountRepository = accountRepository;
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            //Đọc thông tin từ claims
            var identity = context.HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null) 
            {
                var userClaims = identity.Claims;
                var userId = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.PrimarySid)?.Value != null
                    ? Convert.ToInt32(userClaims.FirstOrDefault(x => x.Type == ClaimTypes.PrimarySid)?.Value) : 0;

                if(userId == 0)
                {
                    context.HttpContext.Response.ContentType = "application/json";
                    context.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                    context.Result = new JsonResult(new
                    {
                        ReturnCode = System.Net.HttpStatusCode.Unauthorized,
                        ReturnMessage = "Vui lòng đăng nhập để thực hiện chức năng này "
                    });
                    return;
                }
                //Lấy functionID dựa theo functionCode
                var function = await _accountRepository.GetFunctionIDByName(_functionCode);
                if (function == null)
                {
                    context.HttpContext.Response.ContentType = "application/json";
                    context.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                    context.Result = new JsonResult(new
                    {
                        ReturnCode = System.Net.HttpStatusCode.Unauthorized,
                        ReturnMessage = "Chức năng này không hợp lệ"
                    });
                    return;
                }

                //Lấy quyền từ UserID và FunctionID 
                var permisstion = await _accountRepository.GetPermisstionUserIDOfFunctionID(userId, function.FunctionID);
                if (permisstion == null) 
                {
                    context.HttpContext.Response.ContentType = "application/json";
                    context.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                    context.Result = new JsonResult(new
                    {
                        ReturnCode = System.Net.HttpStatusCode.Unauthorized,
                        ReturnMessage = "Bạn không có quyền thực hiện chức năng này"
                    });
                    return;
                }
                switch (_permission) 
                {
                    case "VIEW":
                        if(permisstion.IsView == 0)
                        {
                            context.HttpContext.Response.ContentType = "application/json";
                            context.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                            context.Result = new JsonResult(new
                            {
                                ReturnCode = System.Net.HttpStatusCode.Unauthorized,
                                ReturnMessage = "Bạn không có quyền thực hiện chức năng này"
                            });
                            return;
                        }
                        break;

                    case "INSERT":
                        if (permisstion.IsInsert == 0)
                        {
                            context.HttpContext.Response.ContentType = "application/json";
                            context.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                            context.Result = new JsonResult(new
                            {
                                ReturnCode = System.Net.HttpStatusCode.Unauthorized,
                                ReturnMessage = "Bạn không có quyền thực hiện chức năng này"
                            });
                            return;
                        }
                        break;
                    case "UPDATE":
                        if (permisstion.IsUpdate == 0)
                        {
                            context.HttpContext.Response.ContentType = "application/json";
                            context.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                            context.Result = new JsonResult(new
                            {
                                ReturnCode = System.Net.HttpStatusCode.Unauthorized,
                                ReturnMessage = "Bạn không có quyền thực hiện chức năng này"
                            });
                            return;
                        }
                        break;

                    case "DELETE":
                        if (permisstion.IsDelete == 0)
                        {
                            context.HttpContext.Response.ContentType = "application/json";
                            context.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                            context.Result = new JsonResult(new
                            {
                                ReturnCode = System.Net.HttpStatusCode.Unauthorized,
                                ReturnMessage = "Bạn không có quyền thực hiện chức năng này"
                            });
                            return;
                        }
                        break;
                }
            }
        }
    }
}
