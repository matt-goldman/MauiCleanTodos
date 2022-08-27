using MauiCleanTodos.ApiClient.Services;
using MauiCleanTodos.Shared.TodoItems;

namespace App.UnitTests.Mocks;
public class MockTodoItemsService : ITodoItemsService
{
    private readonly MockApi _api;

    public MockTodoItemsService(MockApi api)
    {
        _api = api;
    }

    public Task<TodoItemDto> CreateTodoItem(NewTodoItemDto item)
    {
        var rnd = new Random();

        var newDto = new TodoItemDto
        {
            Id = rnd.Next(5, 1000000), // Seeded items go to 4
            ListId = item.ListId,
            Title = item.Title
        };

        var list = _api.TodoLists.FirstOrDefault(l => l.Id == item.ListId);

        list.Items.Add(newDto);

        return Task.FromResult(newDto);
    }

    public Task UpdateTodoItem(TodoItemDto todo)
    {
        // TodoItem will already be passed by reference
        // No need to update anything

        return Task.CompletedTask;
    }
}
