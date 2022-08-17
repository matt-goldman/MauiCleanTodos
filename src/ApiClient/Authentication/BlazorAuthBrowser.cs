using IdentityModel.OidcClient.Browser;
using IBrowser = IdentityModel.OidcClient.Browser.IBrowser;

namespace MauiCleanTodos.ApiClient.Authentication;
public class BlazorAuthBrowser : IBrowser
{
    public Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
