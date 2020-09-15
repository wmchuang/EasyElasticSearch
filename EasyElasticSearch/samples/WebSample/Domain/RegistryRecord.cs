using System;
using EasyElasticSearch.Entity;

namespace WebSample.Domain
{
    public class RegistryRecord : EsBaseEntity
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public DateTime RegistryTime { get; set; } = DateTime.Now;
    }
}