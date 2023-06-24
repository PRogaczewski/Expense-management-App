using Application.Authentication.Models;

namespace Application.Authentication.IServices
{
    public interface IAccountService
    {
        ValueTask ChangePassword(ChangePasswordRequest model);

        ValueTask DeleteAccount();
    }
}
