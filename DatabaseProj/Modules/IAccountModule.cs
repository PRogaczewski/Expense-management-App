using Domain.ValueObjects;

namespace Domain.Modules
{
    public interface IAccountModule
    {
        Task<bool> ChangePassword(ChangePasswordModel model);

        Task<bool> DeleteAccount();
    }
}
