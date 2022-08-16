using IdentityModel.OidcClient.Browser;

namespace MauiCleanTodos.ApiClient.Authentication;
public class BlazorAuthBrowser : IBrowser
{
    public Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
