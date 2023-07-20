namespace Infrastructure.ExternalServices.Models
{
    public class ServiceReponse<T>
    {
        private ServiceReponse(T data)
        {
            Data = data;
        }

        public T Data { get; set; }

        public static ServiceReponse<T> GetResponse(T data)
        {
            return new(data);
        }
    }
}
