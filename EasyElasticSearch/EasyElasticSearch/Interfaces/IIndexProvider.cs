using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyElasticSearch
{
    public interface IIndexProvider
    {
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="index"></param>
        Task InsertAsync<T>(T entity, string index = "") where T : class;

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="index"></param>
        Task InsertRangeAsync<T>(IEnumerable<T> entity, string index = "") where T : class;

        /// <summary>
        /// 判断索引是否存在
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        Task<bool> IndexExistsAsync(string index);

        /// <summary>
        /// 删除索引
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task DeleteIndexAsync<T>() where T : class;
    }
}