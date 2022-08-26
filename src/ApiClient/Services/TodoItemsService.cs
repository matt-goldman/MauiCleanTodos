namespace MauiCleanTodos.ApiClient.Services;

public interface ITodoItemsService
{
    Task UpdateTodoItem(TodoItemDto todo);
}

public class TodoItemsService : BaseService, ITodoItemsService
{
    private readonly TodoItemsClient _client;

    public TodoItemsService(IHttpClientFactory httpClientFactory, ApiClientOptions options) : base(httpClientFactory, options)
    {
        _client = new TodoItemsClient(_baseUrl, _httpClient);


    }

    public async Task UpdateTodoItem(TodoItemDto todo)
    {
        await _client.UpdateAsync(todo.Id, todo);
    }
}
