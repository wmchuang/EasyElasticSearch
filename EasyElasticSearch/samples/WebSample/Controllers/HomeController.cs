using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyElasticSearch;
using Microsoft.AspNetCore.Mvc;
using WebSample.Domain;

namespace WebSample.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HomeController : ControllerBase
    {
        private readonly IEsClientProvider _esClientProvider;

        public HomeController(IEsClientProvider esClientProvider)
        {
            _esClientProvider = esClientProvider;
        }

        public IActionResult Index()
        {
            var s = _esClientProvider.Client.Search<RegistryRecord>(s => s
                            .Index("registryrecord")
                            .From(0)
                            .Size(10)
                            .Query(q => q.Match(t => t.Field(x => x.UserName).Query("es"))));

            return new JsonResult(s);
        }
    }
}
