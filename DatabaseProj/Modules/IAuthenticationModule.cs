using Domain.Entities.Models;

namespace Domain.Modules
{
    public interface IAuthenticationModule
    {
        Task<UserApplication> SignIn(UserApplication model);

        Task<UserApplication> Register(UserApplication model);
    }
}
