using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyElasticSearch
{
    public interface IIndexProvider
    {
        Task<bool> IndexExistsAsync(string index);

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="index"></param>
        Task AddAsync<T>(T entity, string index = "") where T : class;

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="index"></param>
        Task AddManyAsync<T>(IEnumerable<T> entity, string index = "") where T : class;

        Task RemoveIndex(string index);
    }
}
