using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using SearchMvc.Models;

namespace SearchMvc.Services
{
    public class GoogleSearchService : ISearchService
    {
        private IHttpClientWrapper _httpClientWrapper;

        public GoogleSearchService(IHttpClientWrapper httpClientWrapper)
        {
            _httpClientWrapper = httpClientWrapper;
        }
        public async Task<int[]> Search(SearchRequest searchRequest)
        {
            var array = searchRequest.Keywords.Split(',');
            var searchKeywords = array.Where(k => !string.IsNullOrEmpty(k)).Select(k => k.Trim());
            var results = new List<int>();
            
            foreach (var keyword in searchKeywords)
            {
                var requestUrl = $"https://www.google.com.au/search?q={HttpUtility.UrlEncode(keyword)}&num=100";
                var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
                var response = await _httpClientWrapper.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var googleResult = await response.Content.ReadAsStringAsync();
                    results.Add(CalculateMatch(googleResult, searchRequest.Url));
                }
            }

            return results.ToArray();
        }

        private int CalculateMatch(string result, string url)
        {
            var groups = Regex.Matches(result, @"<cite>(.*?)</cite>");
            return groups.Count(g => g.Value.Contains(url));
        }
    }
}
