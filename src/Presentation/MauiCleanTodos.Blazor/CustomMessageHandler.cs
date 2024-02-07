using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace MauiCleanTodos.Blazor;
public class CustomAuthorizationMessageHandler : AuthorizationMessageHandler
{
    public CustomAuthorizationMessageHandler(
        IAccessTokenProvider provider,
        IConfiguration config,
        NavigationManager navigationManager)
        : base(provider, navigationManager)
    {
        var baseAddress = navigationManager.BaseUri;

        ConfigureHandler(
            authorizedUrls: new[] { baseAddress }!,
            scopes: new[] { "email", "profile", "MauiCleanTodos.WebUIAPI" });
    }
}