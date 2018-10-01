using System.ComponentModel.DataAnnotations;

namespace SearchMvc.Models
{
    public class SearchRequest
    {
        [Required]
        public string Keywords { get; set; }
        [Required]
        public string Url { get; set; }
    }
}
