using System;
using EasyElasticSearch;
using Microsoft.AspNetCore.Mvc;
using Nest;
using WebSample.Domain;

namespace WebSample.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IEsClientProvider _esClientProvider;

        public HomeController(IEsClientProvider esClientProvider)
        {
            _esClientProvider = esClientProvider;
        }

        public IActionResult Index()
        {
            Func<SearchDescriptor<RegistryRecord>, ISearchRequest> request = searchDescriptor => searchDescriptor
                             .Index("registryrecord")
                             .From(0)
                             .Size(10)
                             .Query(q => q.Match(t => t.Field(x => x.UserName).Query("es")));


            var s = _esClientProvider.Client.Search(request);

            return new JsonResult(s);
        }
    }
}
