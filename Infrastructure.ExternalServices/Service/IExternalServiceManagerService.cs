using Infrastructure.ExternalServices.HttpClients;
using Infrastructure.ExternalServices.Models;

namespace Infrastructure.ExternalServices.Service
{
    public interface IExternalServiceManagerService<T, TClient> where TClient : IServiceHttpClientPost
    {
        public Task<ServiceReponse<T>> CreateData<TValue>(TValue model) where TValue : class;
    }
}
