using CommunityToolkit.Mvvm.Messaging.Messages;

namespace MauiCleanTodos.ApiClient.Authentication;
public class UserUpdatedMessage : ValueChangedMessage<string>
{
    public UserUpdatedMessage(string value) : base(value)
    {
    }
}
