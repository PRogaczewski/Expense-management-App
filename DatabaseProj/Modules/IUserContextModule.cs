using System.Security.Claims;

namespace Domain.Modules
{
    public interface IUserContextModule
    {
        ClaimsPrincipal UserInfo { get; }

        int? GetUserId();
    }
}
