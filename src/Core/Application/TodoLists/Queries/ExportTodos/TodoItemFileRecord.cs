using MauiCleanTodos.Application.Common.Mappings;
using MauiCleanTodos.Domain.Entities;

namespace MauiCleanTodos.Application.TodoLists.Queries.ExportTodos;

public class TodoItemRecord : IMapFrom<TodoItem>
{
    public string? Title { get; set; }

    public bool Done { get; set; }
}
