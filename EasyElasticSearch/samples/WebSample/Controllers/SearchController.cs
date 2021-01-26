using System.Threading.Tasks;
using EasyElasticSearch;
using Microsoft.AspNetCore.Mvc;
using WebSample.Domain;

namespace WebSample.Controllers
{
    public class SearchController : BaseController
    {
        private readonly IEsClientProvider _clientProvider;
        private readonly ISearchProvider _searchProvider;

        public SearchController(ISearchProvider searchProvider, IEsClientProvider clientProvider)
        {
            _searchProvider = searchProvider;
            _clientProvider = clientProvider;
        }

        [HttpGet]
        public IActionResult SearchAll()
        {
            var data = _searchProvider.Queryable<User>().OrderBy(x => x.Money).ToList();
            return Ok(data);
        }

        [HttpGet]
        public IActionResult SearchPage()
        {
            var data = _searchProvider.Queryable<User>().Where(x => x.UserName == "52").ToPageList(1, 2);
            return Ok(data);
        }

        [HttpGet]
        public IActionResult SearchPageNumber()
        {
            long total = 0;
            var data = _searchProvider.Queryable<User>().Where(x => x.UserName == "52").ToPageList(1, 2, ref total);
            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> SearchAsync()
        {
            var data = await _searchProvider.Queryable<User>().Where(x => x.UserName.Contains("5")).ToListAsync();
            return Ok(data);
        }
    }
}