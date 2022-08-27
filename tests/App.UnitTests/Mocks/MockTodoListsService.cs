using MauiCleanTodos.ApiClient.Services;
using MauiCleanTodos.Shared.PriorityLevels;
using MauiCleanTodos.Shared.TodoLists;

namespace App.UnitTests.Mocks;
public class MockTodoListsService : ITodoListsService
{
    private readonly MockApi _api;

    public List<PriorityLevelDto> PriorityLevels => throw new NotImplementedException();

    public MockTodoListsService(MockApi api)
    {
        _api = api;
    }

    public Task DeleteTodoList(int id)
    {
        _api.TodoLists.RemoveAll(l => l.Id == id);

        return Task.CompletedTask;
    }

    public Task<List<TodoListDto>> GetTodos()
    {
        return Task.FromResult(_api.TodoLists);
    }

    public Task<int> NewTodoList(NewTodoDto newTodo)
    {
        var rnd = new Random();

        var newList = new TodoListDto
        {
            Colour  = newTodo.Colour.ToString(),
            Title   = newTodo.Title,
            Id      = rnd.Next(2, 1000000) // Seeded list is Id 1
        };

        _api.TodoLists.Add(newList);

        return Task.FromResult(newList.Id);
    }
}
