using EasyElasticSearch.Entity;
using System;

namespace WebSample.Domain
{
    public class RegistryRecord : EsBaseEntity
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public DateTime RegistryTime { get; set; }
    }
}
