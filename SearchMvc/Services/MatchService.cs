using System.Linq;
using System.Text.RegularExpressions;

namespace SearchMvc.Services
{
    public class MatchService : IMatchService
    {
        public int Count(string input, string url)
        {
            var groups = Regex.Matches(input, @"<cite>(.*?)</cite>");
            return groups.Count(g => g.Value.Contains(url));
        }
    }
}
