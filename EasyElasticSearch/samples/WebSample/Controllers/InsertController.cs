using System;
using System.Collections.Generic;
using EasyElasticSearch;
using Microsoft.AspNetCore.Mvc;
using WebSample.Domain;

namespace WebSample.Controllers
{
    /// <summary>
    /// 新增操作
    /// </summary>
    public class InsertController : BaseController
    {
        private readonly IIndexProvider _indexProvider;

        public InsertController(IIndexProvider indexProvider)
        {
            _indexProvider = indexProvider;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult InsertAsync()
        {
            var user = new User
            {
                UserId = "A112312312311",
                UserName = $"U{DateTime.Now.Second.ToString()}",
                CreateTime = DateTime.Now,
                Money = 110m
            };
            _indexProvider.InsertAsync(user);
            return Ok("Yes");
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult InsertRangeAsync()
        {
            var users = new List<User>
            {
                new User
                {
                    UserId = "B123123123",
                    UserName = $"U{DateTime.Now.Second.ToString()}",
                    CreateTime = DateTime.Now,
                    Money = 80m
                },
                new User
                {
                    UserId = "B4564123156",
                    UserName = $"U{DateTime.Now.Second.ToString()}",
                    CreateTime = DateTime.Now,
                    Money = 90m
                }
            };
            _indexProvider.InsertRangeAsync(users);
            return Ok("Yes");
        }
    }
}