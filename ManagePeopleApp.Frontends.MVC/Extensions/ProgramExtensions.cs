using HttpClientLibrary.HttpClientService;
using ManagePeopleApp.Frontends.MVC.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ManagePeopleApp.Frontends.MVC.Extensions;
public static class ProgramExtensions
{
    public static IServiceCollection AddSessionServices(this IServiceCollection services)
    {
        services.AddDistributedMemoryCache();
        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30);
        });

        return services;
    }

    public static IServiceCollection AddConfigurationServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<ConnectionStringsConfiguration>(
            configuration.GetSection("ConnectionStrings"));

        services.Configure<AppKeysConfiguration>(
            configuration.GetSection("AppKeys"));

        services.Configure<ApiEndpointsConfiguration>(
            configuration.GetSection("ApiEndpoints"));

        return services;
    }

    public static IServiceCollection AddHttpClientServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        string? baseAddress = configuration["HttpClientBase:StandbyConnectBaseAddress"];

        if (baseAddress is not null)
        {
            services.AddHttpClient<IHttpClientHelper, HttpClientHelper>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Add("XApiKey", configuration["Key:XApiKey"]);
            });
        }

        return services;
    }
}

