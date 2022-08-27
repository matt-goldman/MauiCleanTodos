namespace MauiCleanTodos.ApiClient.Services;

public interface ITodoItemsService
{
    Task UpdateTodoItem(TodoItemDto todo);
    Task<TodoItemDto> CreateTodoItem(NewTodoItemDto item);
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

    public async Task<TodoItemDto> CreateTodoItem(NewTodoItemDto item)
    {
        int id =  await _client.CreateAsync(item);

        return new TodoItemDto
        {
            Done    = false,
            Id      = id,
            ListId  = item.ListId,
            Title   = item.Title
        };
    }
}
