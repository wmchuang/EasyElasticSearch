using System.Collections.Generic;

namespace EasyElasticSearch
{
    public interface IIndexProvider
    {
        bool IndexExists(string index);

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="index"></param>
        void Index<T>(T entity, string index = "") where T : class;

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="index"></param>
        void BulkIndex<T>(List<T> entity, string index = "") where T : class;

        void RemoveIndex(string index);
    }
}
