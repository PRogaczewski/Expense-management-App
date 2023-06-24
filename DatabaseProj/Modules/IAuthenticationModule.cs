namespace Domain.Modules
{
    public interface IAuthenticationModule<T> where T : class
    {
        Task<T> SignIn(T model);

        Task<T> Register(T model);
    }
}
