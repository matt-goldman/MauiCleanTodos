using Microsoft.Extensions.Options;

namespace MauiCleanTodos.ApiClient.Services;

public interface ITodoItemsService
{

}

public class TodoItemsService : BaseService, ITodoItemsService
{
    private readonly TodoItemsClient _client;

    public TodoItemsService(IHttpClientFactory httpClientFactory, IOptions<ApiClientOptions> options) : base(httpClientFactory, options)
    {
        _client = new TodoItemsClient(_baseUrl, _httpClient);
    }
}
