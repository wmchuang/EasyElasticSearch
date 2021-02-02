using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyElasticSearch;
using Microsoft.AspNetCore.Mvc;
using WebSample.Domain;

namespace WebSample.Controllers
{
    /// <summary>
    /// 索引操作
    /// </summary>
    public class IndexController : BaseController
    {
        private readonly IIndexProvider _indexProvider;

        public IndexController(IIndexProvider indexProvider)
        {
            _indexProvider = indexProvider;
        }

        /// <summary>
        /// 删除索引
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> DeleteIndexAsync()
        {
            await _indexProvider.DeleteIndexAsync<UserWallet>();
            return Success();
        }

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> InsertAsync()
        {
            var user = new UserWallet
            {
                UserId = "A112312312311",
                UserName = $"U{DateTime.Now.Second.ToString()}",
                CreateTime = DateTime.Now,
                Money = 110m
            };
            await _indexProvider.InsertAsync(user);
            return Success();
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> InsertRangeAsync()
        {
            var users = new List<UserWallet>
            {
                new UserWallet
                {
                    UserId = "B123123123",
                    UserName = $"U{DateTime.Now.Second.ToString()}",
                    CreateTime = DateTime.Now,
                    Money = 80m
                },
                new UserWallet
                {
                    UserId = "B4564123156",
                    UserName = $"U{DateTime.Now.Second.ToString()}",
                    CreateTime = DateTime.Now,
                    Money = 90m
                }
            };
            await _indexProvider.InsertRangeAsync(users);
            return Success();
        }
       
        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> InsertRange2Async()
        {
            var users = new List<UserWallet>();

            var rd = new Random();
            for (var i = 0; i < 100; i++)
            {
                users.Add(new UserWallet
                {
                    UserId = rd.Next().ToString(),
                    UserName = $"U{DateTime.Now.Second.ToString()}",
                    CreateTime = DateTime.Now,
                    Money = rd.Next(10,100)
                });   
            }
         
            await _indexProvider.InsertRangeAsync(users);
            return Success();
        }
    }
}