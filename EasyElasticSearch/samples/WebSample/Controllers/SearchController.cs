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
            var data = _searchProvider.Queryable<UserWallet>().OrderBy(x => x.Money).ToList();
            return Success(data);
        }

        [HttpGet]
        public IActionResult SearchPage()
        {
            var data = _searchProvider.Queryable<UserWallet>().Where(x => x.UserName == "Update").ToPageList(1, 2);
            return Success(data);
        }

        [HttpGet]
        public IActionResult SearchPageNumber()
        {
            var data = _searchProvider.Queryable<UserWallet>().Where(x => x.UserName == "Update").ToPageList(1, 2, out var total);
            return Success(new
            {
                total,
                data
            });
        }

        [HttpGet]
        public async Task<IActionResult> SearchAsync()
        {
            var data = await _searchProvider.Queryable<UserWallet>().Where(x => x.UserName.Contains("5")).ToListAsync();
            return Success(data);
        }
        
        [HttpGet]
        public async Task<IActionResult> GroupAsync()
        {
            var data = await _searchProvider.Queryable<UserWallet>().GroupBy(x => x.UserName).ToListAsync();
            return Success(data);
        }


        #region NEST

        
        [HttpGet]
        public async Task<IActionResult> NestSearchAsync()
        {
            var data = await _clientProvider.Client.SearchAsync<UserWallet>(x => x.Index("User").Query(q => q.MatchAll()));
            return Success(data.Documents);
        }

        #endregion
    }
}