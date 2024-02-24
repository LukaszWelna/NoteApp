using System.Security.Claims;

namespace NoteApp.Server.Services
{
    public interface IUserContextService
    {
        public ClaimsPrincipal User { get; }

        public int GetUserId {  get; }
    }
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        
        public ClaimsPrincipal User => _httpContextAccessor.HttpContext.User;

        public int GetUserId => int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
    }
}
