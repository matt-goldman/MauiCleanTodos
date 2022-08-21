using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Maui.Views;
using MauiCleanTodos.ApiClient.Authentication;
using MauiCleanTodos.App.PopupPages;
using MauiCleanTodos.Shared.TodoLists;

namespace MauiCleanTodos.App.ViewModels;
public partial class MainViewModel : BaseViewModel, IRecipient<UserUpdatedMessage>
{
	private readonly ITodoListsService _todoListsService;
	private readonly IAuthService _authService;

	[ObservableProperty]
	bool loggedIn;

	[ObservableProperty]
	string userName;

	public ObservableCollection<TodoListDto> TodoLists { get; set; } = new();

	public Page Parent { get; set; }

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
		IsBusy = true;

		LoggedIn = await _authService.LoginAsync();

		if (LoggedIn)
		{
			await RefreshLists();
		}

		IsBusy = false;
	}

	[RelayCommand]
	private async Task AddList()
	{
		var newListPopup = new NewListPopup();

		var result = await Parent.ShowPopupAsync(newListPopup);
	}

	private async Task RefreshLists()
	{
		IsBusy = true;

		var lists = await _todoListsService.GetTodos();

		foreach (var list in lists)
		{
			TodoLists.Add(list);
		}

		IsBusy = false;
	}
}
