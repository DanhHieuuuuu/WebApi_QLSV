using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace WebApi_QLSV.Dtos
{
    public class AuthorizationFilter : Attribute, IAuthorizationFilter
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly string _role;
        public AuthorizationFilter(IHttpContextAccessor contextAccessor, string role)
        {
            _contextAccessor = contextAccessor;
            _role = role;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // get dbcontext in filter Attribute, IAuthorizationFilter
            // hash mask
            var role = CommonUntil.GetCurrentRole(_contextAccessor);
            string[] roles = _role.Split(',');
            int i = 0;
            foreach (var items in roles)
            {
                if(items == role)
                {
                    i++;
                }
            }
            if (i == 0)
            {
                context.Result = new UnauthorizedObjectResult(new { message = $"Bạn không có quyền" });
            }

        }
    }
}
