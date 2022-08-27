using MauiCleanTodos.ApiClient.Authentication;
using MauiCleanTodos.ApiClient.Storage;
using IBrowser = IdentityModel.OidcClient.Browser.IBrowser;

namespace MauiCleanTodos.ApiClient;
public static class DependencyInjection
{
    /// <summary>
    /// Registers the API client, makes an authentication service available for use in your code, and provides a base service with an authenticated HTTP client instance.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="clientOptions">IdentityServer config and API base URI</param>
    /// <param name="browserType">.NET MAUI or Blazor</param>
    /// <returns></returns>
    public static IServiceCollection RegisterMauiClient(this IServiceCollection services, ApiClientOptions clientOptions)
    {
        services.AddSingleton<IBrowser, MauiAuthBroswer>();
        services.AddSingleton<ISecureStorageProvider, MauiStorageProvider>();

        services.AddSingleton(clientOptions);

        services.RegisterApiClientServices();

        return services;
    }
}
