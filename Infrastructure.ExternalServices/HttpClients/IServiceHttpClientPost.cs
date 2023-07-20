namespace Infrastructure.ExternalServices.HttpClients
{
    public interface IServiceHttpClientPost
    {
        Task<string> Execute(StringContent content);
    }
}
