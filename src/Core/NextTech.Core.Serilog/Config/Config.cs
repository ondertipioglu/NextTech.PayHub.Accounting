using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NextTech.Core.Shared.Settings;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.IO;
using System.Reflection;
namespace NextTech.Core.Serilog.Config
{
    public static class SerilogConfig
    {
        public static IHostBuilder UseSerilog(this IHostBuilder webHostBuilder, string applicationName)
        {
            return webHostBuilder.UseSerilog((context, loggerConfiguration) =>
            {
                var config = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: true)
                            .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true)
                            .Build();

                var elasticSettings = config.GetSection(nameof(ElasticSearchSettings)).Get<ElasticSearchSettings>();

                loggerConfiguration
                    .Enrich.FromLogContext()
                    .Enrich.WithProperty("Version", Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyFileVersionAttribute>().Version)
                    .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                    .Enrich.WithProperty("ApplicationName", applicationName)
                    .Enrich.WithProperty("MachineName", Environment.MachineName)
                    ;

                loggerConfiguration
                    .WriteTo.Console()
                    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticSettings.Url))
                    {
                        AutoRegisterTemplate = true,
                        IndexFormat = elasticSettings.LogIndexFormat
                    });
            });
        }
    }
}
