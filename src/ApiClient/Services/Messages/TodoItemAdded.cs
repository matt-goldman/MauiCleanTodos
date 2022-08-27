using CommunityToolkit.Mvvm.Messaging.Messages;

namespace MauiCleanTodos.ApiClient.Services.Messages;
public class TodoItemAdded : AsyncRequestMessage<TodoItemDto>
{
	public NewTodoItemDto Item { get; set; }

	public TodoItemAdded(NewTodoItemDto item)
	{
		Item = item;
	}
}
