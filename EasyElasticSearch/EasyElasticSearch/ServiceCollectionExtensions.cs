using Microsoft.Extensions.DependencyInjection;
using System;

namespace EasyElasticSearch
{
    public static class ServiceCollectionExtensions
    {
        public static void AddEsService(this IServiceCollection services, Action<EsConfig> setupAction)
        {
            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction), "调用 Elasticsearch 配置时出错，未传入配置信息。");
            }
            services.Configure(setupAction);

            services.AddTransient<IEsClientProvider, EsClientProvider>();
            services.AddTransient<IIndexProvider, ElasticsearchProvider>();
            services.AddTransient<ISearchProvider, ElasticsearchProvider>();
        }
    }
}
