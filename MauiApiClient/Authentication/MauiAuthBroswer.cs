using IdentityModel.OidcClient.Browser;
using IBrowser = IdentityModel.OidcClient.Browser.IBrowser;

namespace MauiCleanTodos.ApiClient.Authentication;
public class MauiAuthBroswer : IBrowser
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
        //string sessionState = result.Properties["session_state"];
        return $"{AuthService.RedirectUri}#code={code}&scope={scope}&state={state}";// &session_state={sessionState}";
    }
}
