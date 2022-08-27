using CommunityToolkit.Mvvm.Messaging.Messages;

namespace MauiCleanTodos.ApiClient.Services.Messages;
public class TodoItemUpdated : ValueChangedMessage<TodoItemDto>
{
    public TodoItemUpdated(TodoItemDto value) : base(value)
    {
    }
}
