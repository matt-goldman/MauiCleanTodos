using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Views;
using MauiCleanTodos.App.PopupPages;
using MauiCleanTodos.Shared.TodoItems;
using MauiCleanTodos.Shared.TodoLists;

namespace MauiCleanTodos.App.Controls;

public partial class TodoItemsView : ContentView
{
	private readonly int _listId;
	
	private readonly ITodoItemsService _todoItemsService;

    public ObservableCollection<TodoItemDto> TodoItems { get; set; } = new();

	public TodoItemsView(TodoListDto list, ITodoItemsService todoItemsService)
	{
		InitializeComponent();

		BusyIndicator.IsVisible = false;
		_todoItemsService = todoItemsService;
	}

	protected override void OnSizeAllocated(double width, double height)
	{
		base.OnSizeAllocated(width, height);

		this.BackgroundColor = Colors.Transparent;
	}

	[RelayCommand]
	private async Task ItemChecked(TodoItemDto item)
	{
		if (item is not null)
		{
			await _todoItemsService.UpdateTodoItem(item);
        }
	}

	[RelayCommand]
	private async Task AddItem()
	{
		BusyIndicator.IsVisible = true;

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

			if (newDto is not null)
			{
				TodoItems.Add(newDto);
			}
		}

		BusyIndicator.IsVisible = false;
	}
}