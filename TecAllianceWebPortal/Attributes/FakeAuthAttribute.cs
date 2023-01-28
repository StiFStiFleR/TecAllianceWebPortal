using Microsoft.AspNetCore.Mvc.Filters;
using TecAllianceWebPortal.Services.Interfaces;

namespace TecAllianceWebPortal.Attributes
{
    public class FakeAuthAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            if (context.HttpContext.Request.Cookies.TryGetValue("email", out string? email))
            {
                var userService = (IUserService)context.HttpContext.RequestServices.GetService(typeof(IUserService));
                var user = await userService?.GetUserByEmail(email!);
                if (user != null)
                {
                    await next();
                }
                else
                {
                    context.HttpContext.Response.StatusCode = 401;
                }
            }
            else
            {
                context.HttpContext.Response.StatusCode = 401;
            }
        }
    }
}
