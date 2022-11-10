using System.IdentityModel.Tokens.Jwt;
using IdentityModel.OidcClient;
using MauiCleanTodos.ApiClient;
using IBrowser = IdentityModel.OidcClient.Browser.IBrowser;

namespace MauiCleanTodos.App.Authentication;
public interface IAuthService
{
    Task<bool> LoginAsync();

    Task<bool> RefreshLoginAsync();

    bool Logout();
}

public class AuthService : IAuthService
{
    public const string AuthenticatedClient = "AuthenticatedClient";

    public const string USER_UPDATED_MESSAGE = nameof(USER_UPDATED_MESSAGE);

    public static string RedirectUri { get; set; } = string.Empty;

    private readonly OidcClientOptions _options;
    private readonly IBrowser _browser;

    public AuthService(
        ApiClientOptions options,
        IBrowser browser)
    {
        _options = new OidcClientOptions
        {
            Authority = options.Authority,
            ClientId = options.ClientId,
            Scope = options.Scope,
            RedirectUri = options.RedirectUri,
            Browser = browser
        };
        _browser = browser;
        RedirectUri = options.RedirectUri;
    }

    internal static string AccessToken { get; set; } = String.Empty;

    internal static string RefreshToken { get; set; } = String.Empty;

    internal static string GetToken() => AccessToken;

    public async Task<bool> LoginAsync()
    {
        try
        {
            var oidcClient = new OidcClient(_options);

            var loginResult = await oidcClient.LoginAsync(new LoginRequest());

            if (loginResult.IsError)
            {
                // TODO: inspect and handle error
                return false;
            }

            await SetRefreshToken(loginResult.RefreshToken);
            SetLoggedInState(loginResult?.AccessToken ?? String.Empty, loginResult?.IdentityToken ?? string.Empty);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Login failed");
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public bool Logout()
    {
        try
        {
            ClearTokens();
            AccessToken = String.Empty;
            return true;
        }
        catch (Exception ex)
        {
            // TODO: handle exception
            return false;
        }
    }

    public async Task<bool> RefreshLoginAsync()
    {
        var oidcClient = new OidcClient(_options);

        RefreshToken = await SecureStorage.GetAsync(nameof(RefreshToken));

        if (string.IsNullOrEmpty(RefreshToken))
            return false;

        var result = await oidcClient.RefreshTokenAsync(RefreshToken);

        if (result.IsError)
        {
            // TODO inspect and handle error
            return false;
        }

        await SetRefreshToken(result.RefreshToken);
        SetLoggedInState(result?.AccessToken ?? String.Empty, result?.IdentityToken ?? string.Empty);
        return true;
    }

    private void SetLoggedInState(string token, string idToken)
    {
        AccessToken = token;

        var claims = ParseToken(idToken);

        var userName = claims?.Claims?.FirstOrDefault(c => c.Type == "name")?.Value;

        if (userName is not null)
        {
            MessagingCenter.Send(this, USER_UPDATED_MESSAGE, userName);
        }
    }

    private async Task SetRefreshToken(string token)
    {
        RefreshToken = token;
        await SecureStorage.SetAsync(nameof(RefreshToken), token);
    }

    private void ClearTokens()
    {
        RefreshToken = String.Empty;
        SecureStorage.Remove(nameof(RefreshToken));
    }

    private JwtSecurityToken? ParseToken(string inTtoken)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(inTtoken);

            return token;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
}
