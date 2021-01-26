﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyElasticSearch;
using Microsoft.AspNetCore.Mvc;
using WebSample.Domain;

namespace WebSample.Controllers
{
    public class EsController : BaseController
    {
        #region Ctor

        private readonly IIndexProvider _indexProvider;
        private readonly IDeleteProvider _deleteProvider;
        private readonly IUpdateProvider _updateProvider;
        private readonly IAliasProvider _aliasProvider;

        public EsController(IIndexProvider indexProvider,
            IDeleteProvider deleteProvider,
            IUpdateProvider updateProvider,
            IAliasProvider aliasProvider)
        {
            _indexProvider = indexProvider;
            _deleteProvider = deleteProvider;
            _updateProvider = updateProvider;
            _aliasProvider = aliasProvider;
        }

        #endregion

        #region 删

        [HttpGet]
        public IActionResult Delete()
        {
            _deleteProvider.DeleteByQuery<RegistryRecord>(x => x.UserName == "Bulks1");
            return Ok("Success");
        }

        #endregion

        #region 改

        [HttpGet("{id}")]
        public IActionResult UpdateByKey(string id)
        {
            var record = new RegistryRecord
            {
                UserId = "1458487865768454",
                UserName = "Update"
            };
            _updateProvider.Update(id, record);
            return Ok("Success");
        }

        #endregion


        #region 查

        // [HttpGet]
        // public IActionResult Page()
        // {
        //     var list = new List<string> { "Bulks2", "es" };
        //     var page = new ElasticsearchPage<RegistryRecord>()
        //     {
        //         PageIndex = 1,
        //         PageSize = 100,
        //         Query = x => x.UserName == "es"
        //     };
        //
        //     var data = _searchProvider.SearchPage<RegistryRecord>(page);
        //     data.Hits.ToList().ForEach(x => Console.WriteLine(x.Id));
        //     return new JsonResult(data.Documents);
        // }

        #endregion

        #region 增

        [HttpGet]
        public async Task<ActionResult> Add()
        {
            var record = new RegistryRecord
            {
                UserId = "1268436794379079680",
                UserName = "es",
                RegistryTime = DateTime.Now
            };

            await _indexProvider.InsertAsync(record);
            return Ok("Success");
        }

        /// <summary>
        ///     批量新增
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult BulkAdd()
        {
            var records = new List<RegistryRecord>
            {
                new RegistryRecord
                {
                    UserId = "1268436794379079680",
                    UserName = "Bulks1",
                    RegistryTime = DateTime.Now
                },
                new RegistryRecord
                {
                    UserId = "1268436794379079680",
                    UserName = "Bulges2",
                    RegistryTime = DateTime.Now
                }
            };

            _indexProvider.InsertRangeAsync(records);
            return Ok("Success");
        }

        #endregion

        #region 别名

        [HttpGet]
        public IActionResult AddAlias()
        {
            _aliasProvider.AddAliasAsync<RegistryRecord>("12312321");
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