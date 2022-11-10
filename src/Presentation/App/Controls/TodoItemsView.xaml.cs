using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using MauiCleanTodos.ApiClient.Services.Messages;
using MauiCleanTodos.App.PopupPages;
using MauiCleanTodos.Shared.TodoItems;
using MauiCleanTodos.Shared.TodoLists;

namespace MauiCleanTodos.App.Controls;

public partial class TodoItemsView : ContentView
{
	private readonly int _listId;

    public const string TODO_ITEM_UPDATED_MESSAGE = nameof(TODO_ITEM_UPDATED_MESSAGE);

    public const string TODO_ITEM_ADDED_MESSAGE = nameof(TODO_ITEM_ADDED_MESSAGE);

    public ObservableCollection<TodoItemDto> TodoItems { get; set; } = new();

	public TodoItemsView(TodoListDto list)
	{
		InitializeComponent();
		BindingContext = this;

		foreach(var item in list.Items)
		{
			TodoItems.Add(item);
		}

		_listId = list.Id;

		BusyIndicator.IsVisible = false;
	}

	protected override void OnSizeAllocated(double width, double height)
	{
		base.OnSizeAllocated(width, height);

		this.BackgroundColor = Colors.Transparent;
	}

	[RelayCommand]
	private void ItemChecked(TodoItemDto item)
	{
		if (item is not null)
		{
            MessagingCenter.Send(this, TODO_ITEM_UPDATED_MESSAGE, item);
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

			var newDto = await WeakReferenceMessenger.Default.Send(new TodoItemAdded(newItem));

			if (newDto is not null)
			{
				TodoItems.Add(newDto);
			}
		}

		BusyIndicator.IsVisible = false;
	}
}