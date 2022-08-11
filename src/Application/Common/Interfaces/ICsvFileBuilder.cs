using MauiCleanTodos.Application.TodoLists.Queries.ExportTodos;

namespace MauiCleanTodos.Application.Common.Interfaces;

public interface ICsvFileBuilder
{
    byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}
