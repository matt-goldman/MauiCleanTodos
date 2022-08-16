using MauiCleanTodos.ApiClient.Authentication;
using Microsoft.Extensions.Options;

namespace MauiCleanTodos.ApiClient.Services;
public class BaseService
{
    protected HttpClient _httpClient;

    protected string _baseUrl;

    public BaseService(IHttpClientFactory httpClientFactory, IOptions<ApiClientOptions> options)
    {
        _httpClient = httpClientFactory.CreateClient(AuthService.AuthenticatedClient);

        _baseUrl = options.Value.BaseUrl;
    }
}
