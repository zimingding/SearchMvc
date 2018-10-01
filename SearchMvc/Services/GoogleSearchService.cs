using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Options;
using SearchMvc.Models;

namespace SearchMvc.Services
{
    public class GoogleSearchService : ISearchService
    {
        private IHttpClientWrapper _httpClientWrapper;
        private IMatchService _matchService;
        private GoogleSearchOptions _searchOptions;

        public GoogleSearchService(IHttpClientWrapper httpClientWrapper, IMatchService matchService, IOptions<GoogleSearchOptions> googleSearchOptions)
        {
            _httpClientWrapper = httpClientWrapper;
            _matchService = matchService;
            _searchOptions = googleSearchOptions.Value;
        }
        public async Task<int[]> Search(SearchRequest searchRequest)
        {
            var array = searchRequest.Keywords.Split(',');
            var searchKeywords = array.Select(k => k.Trim());
            var results = new List<int>();
            
            foreach (var keyword in searchKeywords)
            {
                if (keyword==string.Empty)
                    results.Add(0);
                else
                {
                    var requestUrl = $"{_searchOptions.SearchUrl}/search?q={HttpUtility.UrlEncode(keyword)}&num={_searchOptions.ResultSize}";
                    var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
                    var searchResult = await _httpClientWrapper.SendAsync(request);

                    results.Add(_matchService.Count(searchResult, searchRequest.Url));
                }
            }

            return results.ToArray();
        }
    }
}
