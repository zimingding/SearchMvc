using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SearchMvc.Services
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private static readonly HttpClient HttpClient;

        static HttpClientWrapper()
        {
            HttpClient = new HttpClient
            {
                Timeout = new TimeSpan(0, 0, 0, 2)
            };
        }

        public async Task<string> SendAsync(HttpRequestMessage request)
        {
            var response = await HttpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();
            return string.Empty;
        }
    }
}
