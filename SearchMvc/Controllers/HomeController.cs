using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SearchMvc.Models;
using SearchMvc.Services;

namespace SearchMvc.Controllers
{
    public class HomeController : Controller
    {
        private ISearchService _searchService;

        public HomeController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(SearchRequest searchRequest)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Result), new { keywords = searchRequest.Keywords, url = searchRequest.Url });
            }
            else
            {
                return View();
            }
        }

        public async Task<IActionResult> Result(string keywords, string url)
        {
            var searchRequest = new SearchRequest
            {
                Keywords = keywords,
                Url = url
            };
            var result = await _searchService.Search(searchRequest);
            return View(result);
        }
    }
}
