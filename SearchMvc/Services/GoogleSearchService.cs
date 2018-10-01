using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using SearchMvc.Models;

namespace SearchMvc.Services
{
    public class GoogleSearchService : ISearchService
    {
        private IHttpClientWrapper _httpClientWrapper;
        private IMatchService _matchService;

        public GoogleSearchService(IHttpClientWrapper httpClientWrapper, IMatchService matchService)
        {
            _httpClientWrapper = httpClientWrapper;
            _matchService = matchService;
        }
        public async Task<int[]> Search(SearchRequest searchRequest)
        {
            var array = searchRequest.Keywords.Split(',');
            var searchKeywords = array.Where(k => !string.IsNullOrEmpty(k)).Select(k => k.Trim());
            var results = new List<int>();
            
            foreach (var keyword in searchKeywords)
            {
                var requestUrl = $"https://www.google.com.au/search?q={HttpUtility.UrlEncode(keyword)}&num=10";
                var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
                var searchResult = await _httpClientWrapper.SendAsync(request);
                
                results.Add(_matchService.Count(searchResult, searchRequest.Url));
            }

            return results.ToArray();
        }
    }
}
