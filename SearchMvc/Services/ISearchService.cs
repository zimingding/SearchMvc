using System.Threading.Tasks;
using SearchMvc.Models;

namespace SearchMvc.Services
{
    public interface ISearchService
    {
        Task<int[]> Search(SearchRequest request);
    }
}
