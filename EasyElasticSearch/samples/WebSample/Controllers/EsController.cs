using EasyElasticSearch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using WebSample.Domain;

namespace WebSample.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class EsController : ControllerBase
    {
        #region Ctor

        private readonly IIndexProvider _indexProvider;
        private readonly ISearchProvider _searchProvider;
        private readonly IDeleteProvider _deleteProvider;
        private readonly IUpdateProvider _updateProvider;
        private readonly IAliasProvider _aliasProvider;

        private readonly ILogger<EsController> _logger;
        private readonly IEasyEsContext _easyEsContext;

        public EsController(ILogger<EsController> logger,
            IIndexProvider indexProvider,
            ISearchProvider searchProvider,
            IDeleteProvider deleteProvider,
            IUpdateProvider updateProvider,
            IAliasProvider aliasProvider,
            IEasyEsContext easyEsContext)
        {
            _logger = logger;
            _indexProvider = indexProvider;
            _searchProvider = searchProvider;
            _deleteProvider = deleteProvider;
            _updateProvider = updateProvider;
            _aliasProvider = aliasProvider;
            _easyEsContext = easyEsContext;
        }

        #endregion

        #region 查

        [HttpGet]
        public IActionResult Page()
        {
            var m = _easyEsContext.Query<RegistryRecord>().Where(x => x.UserName == "es").ToList();
            // _searchProvider.Queryable<RegistryRecord>().Where<RegistryRecord>(x => x.UserName == "1").OrderBy();
            var list = new List<string> { "Bulkes2", "es" };
            var page = new ElasticsearchPage<RegistryRecord>()
            {
                PageIndex = 1,
                PageSize = 100,
                Query = x => x.UserName.Contains("Bulkes")
            };

            var data = _searchProvider.SearchPage<RegistryRecord>(page);
            data.Hits.ToList().ForEach(x => Console.WriteLine(x.Id));
            return new JsonResult(data.Documents);
        }

        #endregion

        #region 增

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

        #region 删

        [HttpGet]
        public IActionResult Delete()
        {
            _deleteProvider.DeleteByQuery<RegistryRecord>(x => x.UserName == "Bulkes1");
            return Ok("Success");
        }

        #endregion

        #region 改

        [HttpGet("{id}")]
        public IActionResult UpdateByKey(string id)
        {
            var record = new RegistryRecord()
            {
                UserId = "1458487865768454",
                UserName = "Update"
            };
            _updateProvider.Update(id, record);
            return Ok("Success");
        }

        #endregion

        #region 别名

        [HttpGet]
        public IActionResult AddAlias()
        {
            _aliasProvider.AddAlias<RegistryRecord>("12312321");
            return Ok("Success");
        }

        [HttpGet]
        public IActionResult RemoveAlias()
        {
            _aliasProvider.RemoveAlias<RegistryRecord>("12312321");
            return Ok("Success");
        }

        #endregion
    }
}
