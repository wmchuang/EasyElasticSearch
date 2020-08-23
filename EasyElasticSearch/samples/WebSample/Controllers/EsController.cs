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
        private readonly ISearchProvider _searchProvider;

        private readonly ILogger<EsController> _logger;

        public EsController(ILogger<EsController> logger, IIndexProvider indexProvider, ISearchProvider searchProvider)
        {
            _logger = logger;
            _indexProvider = indexProvider;
            _searchProvider = searchProvider;
        }

        #region 增

        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Add()
        {
            var record = new RegistryRecord
            {
                UserId = "1268436794379079680",
                UserName = "es",
                RegistryTime = DateTime.Now
            };

            _indexProvider.Index<RegistryRecord>(record);
            return Ok("Success");
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult BulkAdd()
        {
            var records = new List<RegistryRecord>
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

            _indexProvider.BulkIndex<RegistryRecord>(records);
            return Ok("Success");
        }

        #endregion

        #region 查

        [HttpGet]
        public IActionResult Page()
        {
            var page = new ElasticsearchPage<RegistryRecord>()
            {
                PageIndex = 1,
                PageSize = 100,
                Query = x => x.UserName == "es"
            };

            var data = _searchProvider.SearchPage<RegistryRecord>(page);
            return new JsonResult(data.Documents);
        }

        #endregion
    }
}
