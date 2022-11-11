using MauiCleanTodos.ApiClient.Authentication;
using MauiCleanTodos.ApiClient.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MauiCleanTodos.ApiClient;

public static class DependencyInjection
{
    /// <summary>
    /// Regissters the API client package. Makes services available that will make authenticated calls to the API.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="clientOptions"></param>
    /// <returns></returns>
    public static IServiceCollection RegisterApiClientServices(this IServiceCollection services, Action<ApiClientOptions> clientOptions)
    {
        services.AddSingleton<AuthHandler>();

        services.AddHttpClient(AuthHandler.AUTHENTICATED_CLIENT)
            .AddHttpMessageHandler((s) => s.GetService<AuthHandler>());

        ApiClientOptions options = new();

        clientOptions.Invoke(options);

        services.AddSingleton(options);

        services.AddSingleton<IWeatherService, WeatherService>();
        services.AddSingleton<ITodoListsService, TodoListsService>();
        services.AddSingleton<ITodoItemsService, TodoItemsService>();

        return services;
    }
}
