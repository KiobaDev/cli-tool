using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Security;
using System.Security.Authentication;
using DuckCreek.ComandLine.Tool.Clients.Gpt;
using DuckCreek.ComandLine.Tool.Clients.HtmlFetching;
using DuckCreek.ComandLine.Tool.Clients.Settings;
using Microsoft.Extensions.Configuration;

namespace DuckCreek.ComandLine.Tool.Clients;

public static class HttpClientExtensions
{
    public static IServiceCollection RegisterHttpClients(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IAzureOpenAiClient, AzureOpenAiClient>();
        
        return services.RegisterOptions(config)
            .RegisterHtmlFetcherClient();
    }

    private static IServiceCollection RegisterOptions(this IServiceCollection services, IConfiguration config)
    {
        services.AddOptions<OpenAiOptions>().Bind
        (
            config.GetSection(OpenAiOptions.SectionName)
        );

        return services;
    }
    
    private static IServiceCollection RegisterHtmlFetcherClient(this IServiceCollection services)
    {
        services.AddHttpClient<IHtmlFetcherClient, HtmlFetcherClient>()
            .ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler
            {
                UseCookies = false,
                AutomaticDecompression = DecompressionMethods.All,
                SslOptions = new SslClientAuthenticationOptions
                {
                    EnabledSslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13
                }
            });
        
        return services;
    }
}