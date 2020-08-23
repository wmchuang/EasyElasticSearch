using EasyElasticSearch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using WebSample.Domain;

namespace WebSample.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class EsController : ControllerBase
    {
        private readonly IIndexProvider _indexProvider;

        private readonly ILogger<EsController> _logger;

        public EsController(ILogger<EsController> logger, IIndexProvider indexProvider)
        {
            _logger = logger;
            _indexProvider = indexProvider;
        }

        [HttpGet]
        public ActionResult Add()
        {
            var log = new RegistryRecord
            {
                UserId = "1268436794379079680",
                UserName = "es",
                RegistryTime = DateTime.Now
            };

            _indexProvider.Index<RegistryRecord>(log);
            return Ok("Success");
        }

        [HttpGet]
        public ActionResult BulkAdd()
        {
            var logs = new List<RegistryRecord>
            {
                new RegistryRecord{
                      UserId = "1268436794379079680",
                      UserName = "Bulkes1",
                     RegistryTime = DateTime.Now
                },
                new RegistryRecord{
                      UserId = "1268436794379079680",
                      UserName = "Bulkes2",
                     RegistryTime = DateTime.Now
                },
            };

            _indexProvider.BulkIndex<RegistryRecord>(logs);
            return Ok("Success");
        }
    }
}
