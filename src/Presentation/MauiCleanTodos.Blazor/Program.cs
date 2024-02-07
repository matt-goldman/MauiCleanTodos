using MauiCleanTodos.ApiClient;
using MauiCleanTodos.Blazor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient<CustomAuthorizationMessageHandler>();


builder.Services.RegisterApiClientServices<CustomAuthorizationMessageHandler>(opt =>
{
    opt.Authority = builder.HostEnvironment.BaseAddress;
    opt.BaseUrl = builder.HostEnvironment.BaseAddress;
    //opt.ClientId = "MauiCleanTodos.WebUI";
    opt.RedirectUri = $"{builder.HostEnvironment.BaseAddress}authentication/login-callback";
    //opt.Scope = "MauiCleanTodos.WebUIAPI openid profile offline_access";
});

builder.Services.AddOidcAuthentication(options =>
{
    // Configure your authentication provider options here.
    // For more information, see https://aka.ms/blazor-standalone-auth
    builder.Configuration.Bind("Local", options.ProviderOptions);

    options.ProviderOptions.DefaultScopes.Add("MauiCleanTodos.WebUIAPI");
    options.ProviderOptions.DefaultScopes.Add("offline_access");

    options.ProviderOptions.ResponseType = "code";
});

await builder.Build().RunAsync();
