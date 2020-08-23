using Nest;

namespace EasyElasticSearch
{
    public interface IAliasProvider
    {
        /// <summary>
        /// 添加别名
        /// </summary>
        /// <param name="index"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        BulkAliasResponse AddAlias(string index, string alias);

        BulkAliasResponse AddAlias<T>(string alias) where T : class;

        /// <summary>
        /// 删除别名
        /// </summary>
        /// <param name="index"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        BulkAliasResponse RemoveAlias(string index, string alias);

        BulkAliasResponse RemoveAlias<T>(string alias) where T : class;
    }
}
