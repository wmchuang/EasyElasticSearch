using System;
using EasyElasticSearch.Entity;

namespace WebSample.Domain
{
    public class UserWallet : EsBaseEntity
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public DateTime CreateTime { get; set; }

        public decimal Money { get; set; }
    }

    public class Manager
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public DateTime CreateTime { get; set; }

        public decimal Money { get; set; }
    }
}