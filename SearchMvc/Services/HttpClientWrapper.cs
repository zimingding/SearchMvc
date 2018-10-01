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

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return await HttpClient.SendAsync(request);
        }
    }
}
