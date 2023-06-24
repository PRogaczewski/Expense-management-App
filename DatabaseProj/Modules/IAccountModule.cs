using Domain.ValueObjects;

namespace Domain.Modules
{
    public interface IAccountModule
    {
        ValueTask ChangePassword(ChangePasswordModel model);

        ValueTask DeleteAccount();
    }
}
