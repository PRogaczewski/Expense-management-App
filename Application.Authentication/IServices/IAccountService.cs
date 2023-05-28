using Application.Authentication.Models;

namespace Application.Authentication.IServices
{
    public interface IAccountService
    {
        Task<bool> ChangePassword(ChangePasswordRequest model);

        Task<bool> DeleteAccount();
    }
}
