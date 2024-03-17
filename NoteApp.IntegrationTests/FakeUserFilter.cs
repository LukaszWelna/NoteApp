using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace NoteApp.IntegrationTests
{
    public class FakeUserFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var claimsPrincipal = new ClaimsPrincipal();

            // Seed user with id 1
            claimsPrincipal.AddIdentity(new ClaimsIdentity(
                new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, "1")
                })); ;

            context.HttpContext.User = claimsPrincipal;

            await next();
        }
    }
}
