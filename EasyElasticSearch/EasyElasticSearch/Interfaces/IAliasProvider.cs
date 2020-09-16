using System.Threading.Tasks;
using Nest;

namespace EasyElasticSearch
{
    public interface IAliasProvider
    {
        /// <summary>
        ///     添加别名
        /// </summary>
        /// <param name="index"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        Task<BulkAliasResponse> AddAliasAsync(string index, string alias);

        Task<BulkAliasResponse> AddAliasAsync<T>(string alias) where T : class;

        /// <summary>
        ///     删除别名
        /// </summary>
        /// <param name="index"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        BulkAliasResponse RemoveAlias(string index, string alias);

        BulkAliasResponse RemoveAlias<T>(string alias) where T : class;
    }
}