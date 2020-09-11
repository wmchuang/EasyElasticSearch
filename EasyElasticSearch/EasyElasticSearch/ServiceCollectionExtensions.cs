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
            services.AddTransient<IDeleteProvider, ElasticsearchProvider>();
            services.AddTransient<IUpdateProvider, ElasticsearchProvider>();
            services.AddTransient<IAliasProvider, ElasticsearchProvider>();
            services.AddTransient<IEasyEsContext, EasyEsContext>();
            //services.Add(new ServiceDescriptor(typeof(IEasyEsContext<>), typeof(EasyEsContext<>), ServiceLifetime.Scoped));//泛型注入
            //services.AddSingleton(typeof(IEasyEsContext<>), typeof(EasyEsContext<>));
        }
    }
}
