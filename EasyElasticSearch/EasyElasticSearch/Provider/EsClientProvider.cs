using Elasticsearch.Net;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nest;
using System;
using System.Linq;

namespace EasyElasticSearch
{
    public class EsClientProvider : IEsClientProvider
    {
        public ElasticClient Client { get; }

        public EsClientProvider(IOptions<EsConfig> esConfig,
            ILogger<EsClientProvider> logger)
        {
            try
            {
                var uris = esConfig.Value.Uris;
                if (uris == null || uris.Count < 1)
                {
                    throw new Exception("urls can not be null");
                }

                ConnectionSettings connectionSetting;
                if (uris.Count == 1)
                {
                    var uri = uris.First();
                    connectionSetting = new ConnectionSettings(uri);
                }
                else
                {
                    var connectionPool = new SniffingConnectionPool(uris);
                    connectionSetting = new ConnectionSettings(connectionPool).DefaultIndex("");
                }

                if (!string.IsNullOrWhiteSpace(esConfig.Value.UserName) && !string.IsNullOrWhiteSpace(esConfig.Value.Password))
                    connectionSetting.BasicAuthentication(esConfig.Value.UserName, esConfig.Value.Password);

                Client = new ElasticClient(connectionSetting);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "ElasticSearch Initialized failed.");
                throw ex;
            }
        }
    }
}
