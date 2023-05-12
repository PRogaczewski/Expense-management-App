using Application.Exceptions;
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

            var userClaim = UserInfo.FindFirst(u => u.Type == ClaimTypes.NameIdentifier);

            if (userClaim == null)
                throw new BusinessException("User not authenticated", 401);

            return int.Parse(userClaim.Value);
        }
    }
}
