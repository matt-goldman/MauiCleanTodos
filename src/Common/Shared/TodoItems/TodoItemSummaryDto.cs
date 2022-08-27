namespace MauiCleanTodos.Shared.TodoItems;
public class TodoItemSummaryDto
{
    public int Id { get; set; }

    public int ListId { get; set; }

    public string? Title { get; set; }

    public bool Done { get; set; }
}
