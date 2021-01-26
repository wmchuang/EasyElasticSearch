﻿using System.Threading.Tasks;
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

        public IActionResult Test()
        {
            var data = _clientProvider.Client.Search<User>(x => x.Index("User")
                .Query(q => q.Term("userName", "54")));
            return Ok(data.Documents);
        }

        public IActionResult Index()
        {
            var data = _searchProvider.Queryable<User>().OrderBy(x => x.Money).ToList();
            return Ok(data);
        }

        public IActionResult SearchPage()
        {
            var data = _searchProvider.Queryable<User>().Where(x => x.UserName == "52").ToPageList(1, 2);
            return Ok(data);
        }

        public IActionResult SearchPageNumber()
        {
            long total = 0;
            var data = _searchProvider.Queryable<User>().Where(x => x.UserName == "52").ToPageList(1, 2, ref total);
            return Ok(data);
        }

        public async Task<IActionResult> SearchAsync()
        {
            var data = await _searchProvider.Queryable<User>().Where(x => x.UserName.Contains("5")).ToListAsync();
            return Ok(data);
        }
    }
}