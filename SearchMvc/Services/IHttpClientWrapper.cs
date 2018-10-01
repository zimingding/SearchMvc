using System.Net.Http;
using System.Threading.Tasks;

namespace SearchMvc.Services
{
    public interface IHttpClientWrapper
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }
}
