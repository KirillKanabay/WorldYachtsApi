using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WorldYachts.Data.Entities;

namespace WorldYachtsApi.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string[] _roles;
        public AuthorizeAttribute(params string [] roles)
        {
            _roles = roles;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (User)context.HttpContext.Items["User"];
            if (user == null)
            {
                // not logged in
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            else
            {
                var role = user.Role;
                if (!(_roles.Contains(role)) && _roles.Length > 0)
                {
                    context.Result = new JsonResult(new { message = "Access denied" }) { StatusCode = StatusCodes.Status403Forbidden };
                }
            }
        }
    }
}
