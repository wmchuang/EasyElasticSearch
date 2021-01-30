using System;

namespace EasyElasticSearch.Entity
{
    public class EsBaseEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}