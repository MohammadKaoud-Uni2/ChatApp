using ChatApp.Api.Repos.Abstract;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ChatApp.Api.Data.Helper
{
    public class LoginUserActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var contextResult = await next();
            if (!contextResult.HttpContext.User.Identity.IsAuthenticated)
            {
                return;
            }
            var email=context.HttpContext.User.GetCurrentUserEmail();
            var repo= contextResult.HttpContext.RequestServices.GetService<IUserRepo>();
            var user= await repo.GetUserByEmail(email);
            user.LastActive=DateTime.Now;
           await  repo.SaveChangesAsync();

        }
    }
}
