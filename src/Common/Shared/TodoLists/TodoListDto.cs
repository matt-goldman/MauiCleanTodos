using MauiCleanTodos.Shared.TodoItems;

namespace MauiCleanTodos.Shared.TodoLists;
public class TodoListDto
{
    public TodoListDto()
    {
        Items = new List<TodoItemDto>();
    }

    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Colour { get; set; }

    public IList<TodoItemDto> Items { get; set; }
}
