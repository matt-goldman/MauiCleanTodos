using CommunityToolkit.Mvvm.Messaging;
using MauiCleanTodos.ApiClient.Authentication;

namespace MauiCleanTodos.App.ViewModels;
public partial class MainViewModel : BaseViewModel, IRecipient<UserUpdatedMessage>
{
	private readonly ITodoListsService _todoListsService;
	private readonly IAuthService _authService;

	[ObservableProperty]
	bool loggedIn;

	[ObservableProperty]
	string userName;

	public MainViewModel(ITodoListsService todoListsService, IAuthService authService)
	{
		_todoListsService = todoListsService;
		_authService = authService;
		WeakReferenceMessenger.Default.Register<UserUpdatedMessage>(this);
	}

	public void Receive(UserUpdatedMessage message)
	{
		UserName = message.Value;
	}

	[RelayCommand]
	private async Task Login()
	{
		LoggedIn = await _authService.LoginAsync();
	}
}
