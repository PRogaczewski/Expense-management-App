using Infrastructure.ExternalServices.HttpClients;
using Infrastructure.ExternalServices.Models;
using Newtonsoft.Json;
using System.Text;

namespace Infrastructure.ExternalServices.Service
{
    public class ExternalServiceManagerService<T, TClient> : IExternalServiceManagerService<T, TClient> where TClient : IServiceHttpClientPost
    {
        private readonly TClient _httpService;

        public ExternalServiceManagerService(TClient httpService)
        {
            _httpService = httpService;
        }

        public async Task<ServiceReponse<T>> CreateData<TValue>(TValue model) where TValue : class
        {
            var content = new StringContent(SerializeData(model), Encoding.UTF8, "application/json");

            var res = DeserializeData<T>(await _httpService.Execute(content));

            return ServiceReponse<T>.GetResponse(res);
        }

        private string SerializeData<TModel>(TModel data)
        {
            return JsonConvert.SerializeObject(data);
        }

        private T DeserializeData<TModel>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}
