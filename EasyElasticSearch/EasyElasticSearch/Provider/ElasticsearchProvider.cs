using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EasyElasticSearch
{
    public class ElasticsearchProvider : IIndexProvider, ISearchProvider, IDeleteProvider, IUpdateProvider, IAliasProvider
    {
        public IElasticClient _elasticClient;

        public ElasticsearchProvider(IEsClientProvider esClientProvider)
        {
            _elasticClient = esClientProvider.Client;
        }

        #region Index

        public bool IndexExists(string index)
        {
            var res = _elasticClient.Indices.Exists(index);
            return res.Exists;
        }

        public void Index<T>(T entity, string index = "") where T : class
        {
            var indexName = index.GetIndex<T>();
            if (!IndexExists(indexName))
            {
                ((ElasticClient)_elasticClient).CreateIndex<T>(indexName);
            }
            var response = _elasticClient.Index(entity,
                s => s.Index(indexName));

            if (!response.IsValid)
                throw new Exception("新增数据失败:" + response.OriginalException.Message);
        }

        public void BulkIndex<T>(List<T> entity, string index) where T : class
        {
            var indexName = index.GetIndex<T>();
            if (!IndexExists(indexName))
            {
                ((ElasticClient)_elasticClient).CreateIndex<T>(indexName);
            }

            var bulkRequest = new BulkRequest(indexName)
            {
                Operations = new List<IBulkOperation>()
            };
            var idxops = entity.Select(o => new BulkIndexOperation<T>(o)).Cast<IBulkOperation>().ToList();
            bulkRequest.Operations = idxops;
            var response = _elasticClient.Bulk(bulkRequest);

            if (!response.IsValid)
                throw new Exception("新增数据失败:" + response.OriginalException.Message);
        }

        public void RemoveIndex(string index)
        {
            if (!IndexExists(index)) return;
            var response = _elasticClient.Indices.Delete(index);

            if (!response.IsValid)
                throw new Exception("删除index失败:" + response.OriginalException.Message);
        }

        #endregion

        #region Search

        public ISearchResponse<T> SearchPage<T>(ElasticsearchPage<T> page) where T : class, new()
        {
            var rquest = page.InitSearchRequest();
            rquest.Query = ExpressionsGetQuery.GetQuery<T>(page.Query);
            var response = _elasticClient.Search<T>(rquest);

            if (!response.IsValid)
                throw new Exception($"查询失败:{response.OriginalException.Message}");
            return response;
        }

        #endregion

        #region Delete

        public DeleteByQueryResponse DeleteByQuery<T>(Expression<Func<T, bool>> expression, string index = "") where T : class, new()
        {
            var indexName = index.GetIndex<T>();
            var request = new DeleteByQueryRequest<T>(indexName)
            {
                Query = ExpressionsGetQuery.GetQuery<T>(expression)
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

        #endregion
    }
}
