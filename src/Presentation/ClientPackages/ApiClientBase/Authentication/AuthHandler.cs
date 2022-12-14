using System.Net.Http.Headers;

namespace MauiCleanTodos.ApiClient.Authentication;
public class AuthHandler : DelegatingHandler
{
    public const string AUTHENTICATED_CLIENT = nameof(AUTHENTICATED_CLIENT);

    private static string _accessToken = string.Empty;

    public static void SetAccessToken(string token)
    {
        Console.WriteLine($"[AuthHandler] Setting access token {token}");
        _accessToken = token;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"[AuthHandler] Sending authenticated request with token {_accessToken}");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

        return await base.SendAsync(request, cancellationToken);
    }
}
