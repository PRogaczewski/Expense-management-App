using Domain.Entities.Models;

namespace Domain.Modules
{
    public interface IAuthenticationModule
    {
        UserApplication SignIn(UserApplication model);

        UserApplication Register(UserApplication model);
    }
}
