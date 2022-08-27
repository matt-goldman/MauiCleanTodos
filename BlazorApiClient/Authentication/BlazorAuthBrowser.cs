using IdentityModel.OidcClient.Browser;

namespace BlazorApiClient.Authentication;

public class BlazorAuthBrowser : IBrowser
{
    public Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
