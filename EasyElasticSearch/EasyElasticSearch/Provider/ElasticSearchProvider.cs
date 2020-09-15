using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Nest;

namespace EasyElasticSearch
{
    public class ElasticSearchProvider : IIndexProvider, IDeleteProvider, IUpdateProvider, IAliasProvider
    {
        private readonly IElasticClient _elasticClient;

        public ElasticSearchProvider(IEsClientProvider esClientProvider)
        {
            _elasticClient = esClientProvider.Client;
        }

        #region Delete

        public DeleteByQueryResponse DeleteByQuery<T>(Expression<Func<T, bool>> expression, string index = "")
            where T : class, new()
        {
            var indexName = index.GetIndex<T>();
            var request = new DeleteByQueryRequest<T>(indexName)
            {
               // Query = ExpressionsGetQuery.GetQuery(expression)
            };
            var response = _elasticClient.DeleteByQuery(request);
            if (!response.IsValid)
                throw new Exception("删除失败:" + response.OriginalException.Message);
            return response;
        }

        #endregion

        #region Update

        public IUpdateResponse<T> Update<T>(string key, T entity, string index = "") where T : class
        {
            var indexName = index.GetIndex<T>();
            var request = new UpdateRequest<T, object>(indexName, key)
            {
                Doc = entity
            };

            var response = _elasticClient.Update(request);
            if (!response.IsValid)
                throw new Exception("更新失败:" + response.OriginalException.Message);
            return response;
        }

        #endregion

        #region Index

        public async Task<bool> IndexExistsAsync(string index)
        {
            var res = await _elasticClient.Indices.ExistsAsync(index);
            return res.Exists;
        }

        public async Task AddAsync<T>(T entity, string index = "") where T : class
        {
            var indexName = index.GetIndex<T>();
            var exists = await IndexExistsAsync(indexName);
            if (!exists) await ((ElasticClient) _elasticClient).CreateIndexAsync<T>(indexName);

            var response = await _elasticClient.IndexAsync(entity,
                s => s.Index(indexName));

            if (!response.IsValid)
                throw new Exception("新增数据失败:" + response.OriginalException.Message);
        }

        public async Task AddManyAsync<T>(IEnumerable<T> entity, string index) where T : class
        {
            var indexName = index.GetIndex<T>();
            var exists = await IndexExistsAsync(indexName);
            if (!exists) await ((ElasticClient) _elasticClient).CreateIndexAsync<T>(indexName);

            var bulkRequest = new BulkRequest(indexName)
            {
                Operations = new List<IBulkOperation>()
            };
            var operations = entity.Select(o => new BulkIndexOperation<T>(o)).Cast<IBulkOperation>().ToList();
            bulkRequest.Operations = operations;
            var response = await _elasticClient.BulkAsync(bulkRequest);

            if (!response.IsValid)
                throw new Exception("批量新增数据失败:" + response.OriginalException.Message);
        }

        public async Task RemoveIndex(string index)
        {
            var exists = await IndexExistsAsync(index);
            if (!exists) return;
            var response = _elasticClient.Indices.Delete(index);

            if (!response.IsValid)
                throw new Exception("删除index失败:" + response.OriginalException.Message);
        }

        #endregion

        #region Alias

        public BulkAliasResponse AddAlias(string index, string alias)
        {
            var response = _elasticClient.Indices.BulkAlias(b => b.Add(al => al
                .Index(index)
                .Alias(alias)));

            if (!response.IsValid)
                throw new Exception("添加Alias失败:" + response.OriginalException.Message);
            return response;
        }

        public BulkAliasResponse AddAlias<T>(string alias) where T : class
        {
            return AddAlias(string.Empty.GetIndex<T>(), alias);
        }

        public BulkAliasResponse RemoveAlias(string index, string alias)
        {
            var response = _elasticClient.Indices.BulkAlias(b => b.Remove(al => al
                .Index(index)
                .Alias(alias)));

            if (!response.IsValid)
                throw new Exception("删除Alias失败:" + response.OriginalException.Message);
            return response;
        }

        public BulkAliasResponse RemoveAlias<T>(string alias) where T : class
        {
            return RemoveAlias(string.Empty.GetIndex<T>(), alias);
        }

        public ISearchRequest<T> OrderBy<T>(Expression<Func<T, object>> expression, OrderByType type = OrderByType.Asc)
            where T : class
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}