using EasyElasticSearch;
using Microsoft.AspNetCore.Mvc;
using WebSample.Domain;

namespace WebSample.Controllers
{
    public class SearchController : BaseController
    {
        private readonly ISearchProvider _searchProvider;

        public SearchController(ISearchProvider searchProvider)
        {
            _searchProvider = searchProvider;
        }

        public IActionResult Index()
        {
            var data = _searchProvider.Queryable<RegistryRecord>().Where(x => x.UserName == "es").ToList();
            return Ok(data);
        }
    }
}