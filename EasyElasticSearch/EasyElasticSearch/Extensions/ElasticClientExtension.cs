using Nest;
using System;

namespace EasyElasticSearch
{
    public static class ElasticClientExtension
    {
        public static bool CreateIndex<T>(this ElasticClient elasticClient, string indexName = "", int numberOfShards = 5, int numberOfReplicas = 1) where T : class
        {
            if (string.IsNullOrWhiteSpace(indexName))
            {
                indexName = typeof(T).Name;
            }

            if (elasticClient.Indices.Exists(indexName).Exists)
            {
                return false;
            }
            else
            {
                var indexState = new IndexState()
                {
                    Settings = new IndexSettings()
                    {
                        NumberOfReplicas = numberOfReplicas,
                        NumberOfShards = numberOfShards,
                    },
                };

                var response = elasticClient.Indices.Create(indexName, p => p.InitializeUsing(indexState).Map<T>(x => x.AutoMap()));
                if (!response.IsValid)
                    throw new Exception($"创建失败:{response.OriginalException.Message}");
                return response.Acknowledged;
            }
        }

      
    }
}
