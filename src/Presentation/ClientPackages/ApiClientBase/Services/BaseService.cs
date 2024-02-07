namespace MauiCleanTodos.ApiClient.Services;
public class BaseService
{
    protected HttpClient _httpClient;

    protected string _baseUrl;

    public BaseService(IHttpClientFactory httpClientFactory, ApiClientOptions options)
    {
        _httpClient = httpClientFactory.CreateClient(Constants.AUTHENTICATED_CLIENT);

        _baseUrl = options.BaseUrl;
    }
}
