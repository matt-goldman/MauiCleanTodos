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
    public static IServiceCollection RegisterApiClient(this IServiceCollection services, ApiClientOptions clientOptions)
    {
        services.AddSingleton<IBrowser, MauiAuthBroswer>();
        services.AddSingleton<ISecureStorageProvider, MauiStorageProvider>();

        //services.AddOptions<ApiClientOptions>()
        //    .Configure(opt =>
        //    {
        //        opt.ClientId    = clientOptions.ClientId;
        //        opt.RedirectUri = clientOptions.RedirectUri;
        //        opt.Authority   = clientOptions.Authority;
        //        opt.BaseUrl     = clientOptions.BaseUrl;
        //        opt.Scope       = clientOptions.Scope;
        //    });

        services.AddSingleton(clientOptions);

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
