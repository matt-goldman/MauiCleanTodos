using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Views;
using MauiCleanTodos.App.Authentication;
using MauiCleanTodos.App.Controls;
using MauiCleanTodos.App.PopupPages;
using MauiCleanTodos.Shared.TodoItems;
using MauiCleanTodos.Shared.TodoLists;

namespace MauiCleanTodos.App.ViewModels;
public partial class MainViewModel : BaseViewModel
{
	private readonly ITodoListsService _todoListsService;
	private readonly ITodoItemsService _todoItemsService;
	private readonly IAuthService _authService;
    private readonly IBottomSheet _bottomSheet;
    
    [ObservableProperty]
	bool loggedIn;

	[ObservableProperty]
	string userName;

	public ObservableCollection<TodoListDto> TodoLists { get; set; } = new();

	public ObservableCollection<TodoItemDto> TodoItems { get; set; } = new();

	private int _listId;

	public MainViewModel(
		ITodoListsService todoListsService,
		ITodoItemsService todoItemsService,
		IAuthService authService,
		IBottomSheet bottomSheet)
	{
		_todoListsService = todoListsService;
		_todoItemsService = todoItemsService;
		_authService = authService;
        _bottomSheet = bottomSheet;

        MessagingCenter.Subscribe<AuthService, string>(this, AuthService.USER_UPDATED_MESSAGE, (sender, arg) => UserName = arg);
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

		var result = await App.Current.MainPage.ShowPopupAsync(newListPopup);

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
		var result = await App.Current.MainPage.ShowPopupAsync(confirmPopup);

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
		_listId = listId;
		var selectedList = TodoLists.FirstOrDefault(l => l.Id == listId);

		TodoItems.Clear();

		foreach (var item in selectedList.Items)
		{
			TodoItems.Add(item);
		}

		var itemsView = new TodoItemsView();

		itemsView.BindingContext = this;

		_bottomSheet.ShowBottomSheet(itemsView);
	}

    [RelayCommand]
    private async Task ItemChecked(TodoItemDto item)
    {
		if (item is not null)
		{
			await _todoItemsService.UpdateTodoItem(item);

			await RefreshLists();
		}
    }

    [RelayCommand]
    private async Task AddItem()
    {
        IsBusy = true;

        var newItemPopup = new AddTodoPopup();

        var newTitle = await App.Current.MainPage.ShowPopupAsync(newItemPopup);

        if (newTitle is not null)
        {
            var newItem = new NewTodoItemDto
            {
                ListId = _listId,
                Title = (string)newTitle
            };

			var newDto = await _todoItemsService.CreateTodoItem(newItem);

            TodoItems.Add(newDto);

			await RefreshLists();
        }

        IsBusy = false;
    }

    public async Task RefreshLists()
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
