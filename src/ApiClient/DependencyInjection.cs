using MauiCleanTodos.ApiClient.Authentication;
using MauiCleanTodos.ApiClient.Services;
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
    public static IServiceCollection RegisterApiClient(this IServiceCollection services, ApiClientOptions clientOptions, BrowserType browserType)
    {
        if (browserType == BrowserType.Blazor)
        {
            services.AddSingleton<IBrowser, BlazorAuthBrowser>();
            services.AddSingleton<ISecureStorageProvider, BlazorStorageProvider>();
        }
        else if (browserType == BrowserType.Maui)
        {
            services.AddSingleton<IBrowser, MauiAuthBroswer>();
            services.AddSingleton<ISecureStorageProvider, MauiStorageProvider>();
        }

        services.AddOptions<ApiClientOptions>();

        services.PostConfigure<ApiClientOptions>(opt => opt = clientOptions);

        services.AddSingleton<AuthHandler>();

        services.AddHttpClient(AuthService.AuthenticatedClient)
            .AddHttpMessageHandler((s) => s.GetService<AuthHandler>());

        services.AddSingleton<IAuthService, AuthService>();
        services.AddSingleton<IWeatherService, WeatherService>();
        services.AddSingleton<ITodoListsService, TodoListsService>();
        services.AddSingleton<ITodoItemsService, TodoItemsService>();

        return services;
    }
}
