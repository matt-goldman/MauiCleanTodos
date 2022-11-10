using IdentityModel.OidcClient.Browser;
using MauiCleanTodos.ApiClient.Authentication;
using IBrowser = IdentityModel.OidcClient.Browser.IBrowser;

namespace MauiCleanTodos.App.Authentication;
public class MauiAuthBrowser : IBrowser
{
    public async Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
    {
        WebAuthenticatorResult result = await WebAuthenticator.AuthenticateAsync(new Uri(options.StartUrl), new Uri(AuthService.RedirectUri));

        return new BrowserResult()
        {
            Response = ParseAuthenticationResult(result)
        };
    }

    private string ParseAuthenticationResult(WebAuthenticatorResult result)
    {
        string code = result.Properties["code"];
        string scope = result.Properties["scope"];
        string state = result.Properties["state"];
        return $"{AuthService.RedirectUri}#code={code}&scope={scope}&state={state}";
    }
}
