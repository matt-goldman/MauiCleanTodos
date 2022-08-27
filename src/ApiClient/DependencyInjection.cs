using MauiCleanTodos.ApiClient.Authentication;
using MauiCleanTodos.ApiClient.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MauiCleanTodos.ApiClient;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApiClientServices(this IServiceCollection services)
    {
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
