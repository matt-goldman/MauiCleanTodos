using BlazorApiClient.Authentication;
using BlazorApiClient.Storage;
using IdentityModel.OidcClient.Browser;
using MauiCleanTodos.ApiClient;
using MauiCleanTodos.ApiClient.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorApiClient;

public static class DependencyInjection
{
    /// <summary>
    /// Registers the API client, makes an authentication service available for use in your code, and provides a base service with an authenticated HTTP client instance.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection RegisterBlazorClient(this IServiceCollection services)
    {
        services.AddSingleton<IBrowser, BlazorAuthBrowser>();
        services.AddSingleton<ISecureStorageProvider, BlazorStorageProvider>();
        
        services.RegisterApiClientServices();

        return services;
    }
}
