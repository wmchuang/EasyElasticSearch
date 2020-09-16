using System;
using System.Collections.Generic;
using EasyElasticSearch;
using Microsoft.AspNetCore.Mvc;
using WebSample.Domain;

namespace WebSample.Controllers
{
    public class IndexController : BaseController
    {
        private readonly IIndexProvider _indexProvider;

        public IndexController(IIndexProvider indexProvider)
        {
            _indexProvider = indexProvider;
        }

        public IActionResult InsertAsync()
        {
            var user = new Manager
            {
                UserId = "123123123",
                UserName = DateTime.Now.Minute.ToString(),
                CreateTime = DateTime.Now,
                Money = 110m
            };
            _indexProvider.InsertAsync(user);
            return Ok("Yes");
        }

        public IActionResult InsertRangeAsync()
        {
            var users = new List<User>
            {
                new User
                {
                    UserId = "123123123",
                    UserName = DateTime.Now.Minute.ToString(),
                    CreateTime = DateTime.Now,
                    Money = 80m
                },
                new User
                {
                    UserId = "456456",
                    UserName = DateTime.Now.Minute.ToString(),
                    CreateTime = DateTime.Now,
                    Money = 90m
                }
            };
            _indexProvider.InsertRangeAsync(users);
            return Ok("Yes");
        }
    }
}