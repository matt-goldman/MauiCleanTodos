using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Maui.Views;
using MauiCleanTodos.ApiClient.Authentication;
using MauiCleanTodos.App.PopupPages;
using MauiCleanTodos.Shared.TodoLists;
using BottomSheet;
using MauiCleanTodos.App.Controls;
using MauiCleanTodos.ApiClient.Services.Messages;
using MauiCleanTodos.Shared.TodoItems;
using Maui.Plugins.PageResolver;

namespace MauiCleanTodos.App.ViewModels;
public partial class MainViewModel : BaseViewModel, IRecipient<UserUpdatedMessage>
{
	private readonly ITodoListsService _todoListsService;
	private readonly ITodoItemsService _todoItemsService;
	private readonly IAuthService _authService;

	[ObservableProperty]
	bool loggedIn;

	[ObservableProperty]
	string userName;

	public ObservableCollection<TodoListDto> TodoLists { get; set; } = new();

	public Page Parent { get; set; }

	public MainViewModel(ITodoListsService todoListsService, ITodoItemsService todoItemsService, IAuthService authService)
	{
		_todoListsService = todoListsService;
		_todoItemsService = todoItemsService;
		_authService = authService;
		WeakReferenceMessenger.Default.Register(this);

		WeakReferenceMessenger.Default.Register<TodoItemUpdated>(this, async (r, m) => await UpdateTodoItem(m.Value));

	}

	public void Receive(UserUpdatedMessage message)
	{
		UserName = message.Value;
	}

    private async Task UpdateTodoItem(TodoItemDto item)
    {
		await _todoItemsService.UpdateTodoItem(item);
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
	}

	[RelayCommand]
	private async Task AddList()
	{
		IsBusy = true;

		var newListPopup = new NewListPopup();

		var result = await Parent.ShowPopupAsync(newListPopup);

		if (result is not null)
		{
			var newTodo = result as NewTodoDto;

			await _todoListsService.NewTodoList(newTodo);

			await RefreshLists(); 
		}

		IsBusy = false;
	}

	[RelayCommand]
	private async Task Delete(int id)
	{
		var confirmPopup = new ConfirmPopup("Delete list", true);
		var result = await Parent.ShowPopupAsync(confirmPopup);

		bool sure = (bool?)result ?? false;

		if (sure)
		{
			IsBusy = true;

			await _todoListsService.DeleteTodoList(id);

			await RefreshLists();
		}
	}

	[RelayCommand]
	private void ShowList(int listId)
	{
		var selectedList = TodoLists.FirstOrDefault(l => l.Id == listId);

		var itemsView = new TodoItemsView(selectedList.Items.ToList());

		//itemsView.BackgroundColor = Colors.Transparent;

        Parent.ShowBottomSheet(itemsView, true);
	}

	private async Task RefreshLists()
	{
		TodoLists.Clear();
		var lists = await _todoListsService.GetTodos();

		foreach (var list in lists)
		{
			TodoLists.Add(list);
		}

		IsBusy = false;
	}
}
