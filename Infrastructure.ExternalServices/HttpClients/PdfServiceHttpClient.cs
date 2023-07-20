namespace Infrastructure.ExternalServices.HttpClients
{
    public class PdfServiceHttpClient : IServiceHttpClientPost
    {
        private readonly HttpClient Client;

        public PdfServiceHttpClient(HttpClient client)
        {
            Client = client;
            Client.BaseAddress = new Uri("https://localhost:7253");
            Client.Timeout = new TimeSpan(0, 1, 0);
            Client.DefaultRequestHeaders.Clear();
        }

        public async Task<string> Execute(StringContent content)
        {
            using (var response = await Client.PostAsync("/PdfDocument", content))
            {
                response.EnsureSuccessStatusCode();

                var resultString = await response.Content.ReadAsStringAsync();

                return resultString;
            }
        }
    }
}
