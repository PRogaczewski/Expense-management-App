using Domain.Modules;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure.Authentication.Service
{
    public class UserContextService : IUserContextModule
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal UserInfo => _httpContextAccessor.HttpContext?.User; 

        public int? GetUserId()
        {
            if (UserInfo == null)
                return null;

            return int.Parse(UserInfo.FindFirst(u => u.Type == ClaimTypes.NameIdentifier).Value);
        }
    }
}
