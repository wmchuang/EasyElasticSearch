using System;

namespace EasyElasticSearch.Entity.Mapping
{
    public class MappingColumn
    {
        public Type PropertyInfo { get; set; }

        public string PropertyName { get; set; }

        public string SearchName { get; set; }
    }
}