using Microsoft.Extensions.Options;

namespace MauiCleanTodos.ApiClient.Services;
public interface ITodoListsService
{

}

public class TodoListsService : BaseService, ITodoListsService
{
    private readonly TodoListsClient _client;

    public TodoListsService(IHttpClientFactory httpClientFactory, IOptions<ApiClientOptions> options) : base(httpClientFactory, options)
    {
        _client = new TodoListsClient(options.Value.BaseUrl, _httpClient);
    }
}
