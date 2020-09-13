using EasyElasticSearch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        public IActionResult AddAsync()
        {
            var user = new User
            {
                UserId = "123123123",
                UserName = "MC",
                CreateTime = DateTime.Now,
                Money = 100m
            };
            _indexProvider.AddAsync(user);
            return Ok("Yes");
        }

        public IActionResult AddManyAsync()
        {
            var users = new List<User>
            {
               new User
                {
                    UserId = "123123123",
                    UserName = "MC",
                    CreateTime = DateTime.Now,
                    Money = 100m
                },
               new User
                {
                    UserId = "456456",
                    UserName = "MC",
                    CreateTime = DateTime.Now,
                    Money = 120m
                },
            };
            _indexProvider.AddManyAsync(users);
            return Ok("Yes");
        }
    }
}