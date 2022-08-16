using System.IdentityModel.Tokens.Jwt;
using CommunityToolkit.Mvvm.Messaging;
using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;
using MauiCleanTodos.ApiClient.Storage;
using Microsoft.Extensions.Options;

namespace MauiCleanTodos.ApiClient.Authentication;

public interface IAuthService
{
    Task<bool> LoginAsync();

    Task<bool> RefreshLoginAsync();

    bool Logout();
}

public class AuthService : IAuthService
{
    public const string AuthenticatedClient = "AuthenticatedClient";

    private readonly OidcClientOptions _options;
    private readonly ISecureStorageProvider _secureStorageProvider;
    private readonly IMessenger _messenger;

    public AuthService(
        IOptions<ApiClientOptions> options,
        IBrowser browser,
        ISecureStorageProvider secureStorageProvider,
        IMessenger messenger)
    {
        _options = new OidcClientOptions
        {
            Authority = options.Value.Authority,
            ClientId = options.Value.ClientId,
            Scope = options.Value.Scope,
            RedirectUri = options.Value.RedirectUri,
            Browser = browser
        };
        _secureStorageProvider = secureStorageProvider;
        _messenger = messenger;
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
            SetLoggedInState(loginResult?.AccessToken ?? String.Empty);
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

        RefreshToken = await _secureStorageProvider.GetAsync(nameof(RefreshToken));

        if (string.IsNullOrEmpty(RefreshToken))
            return false;

        var result = await oidcClient.RefreshTokenAsync(RefreshToken);

        if (result.IsError)
        {
            // TODO inspect and handle error
            return false;
        }

        await SetRefreshToken(result.RefreshToken);
        SetLoggedInState(result?.AccessToken ?? String.Empty);
        return true;
    }

    private void SetLoggedInState(string token)
    {
        AccessToken = token;

        var claims = ParseToken(token);

        var userName = claims?.Claims?.FirstOrDefault(c => c.Type == "Username")?.Value;

        if (userName is not null)
        {
            _messenger.Send(new UserUpdatedMessage(userName));
        }
    }

    private async Task SetRefreshToken(string token)
    {
        RefreshToken = token;
        await _secureStorageProvider.SaveAsync(nameof(RefreshToken), token);
    }

    private void ClearTokens()
    {
        RefreshToken = String.Empty;
        _secureStorageProvider.Remove(nameof(RefreshToken));
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
