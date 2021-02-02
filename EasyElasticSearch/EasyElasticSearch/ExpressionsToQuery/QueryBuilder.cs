using System;
using System.Linq.Expressions;
using EasyElasticSearch.Entity.Mapping;
using Nest;

namespace EasyElasticSearch
{
    /// <summary>
    /// QueryContainer建造者
    /// </summary>
    public class QueryBuilder<T>
    {
        private readonly MappingIndex _mappingIndex;

        public QueryBuilder()
        {
            _mappingIndex = InitMappingInfo();
        }

        public QueryContainer GetQueryContainer(Expression<Func<T, bool>> expression)
        {
            return ExpressionsGetQuery.GetQuery(expression, _mappingIndex);
        }
        
        public MappingIndex GetMappingIndex()
        {
            return _mappingIndex;
        }

        private static MappingIndex InitMappingInfo()
        {
            return InitMappingInfo(typeof(T));
        }

        private static MappingIndex InitMappingInfo(Type type)
        {
            var mapping = new MappingIndex {Type = type, IndexName = type.Name};
            foreach (var property in type.GetProperties())
                mapping.Columns.Add(new MappingColumn
                {
                    PropertyInfo = property.PropertyType,
                    PropertyName = property.Name,
                    SearchName = FiledHelp.GetValues(property.PropertyType.Name, property.Name)
                });
            return mapping;
        }
    }
}