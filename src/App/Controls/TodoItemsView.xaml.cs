using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Messaging;
using MauiCleanTodos.ApiClient.Services.Messages;
using MauiCleanTodos.Shared.TodoItems;

namespace MauiCleanTodos.App.Controls;

public partial class TodoItemsView : ContentView
{
	public ObservableCollection<TodoItemDto> TodoItems { get; set; } = new();

	public TodoItemsView(List<TodoItemDto> items)
	{
		InitializeComponent();
		BindingContext = this;

		foreach(var item in items)
		{
			TodoItems.Add(item);
		}
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
            WeakReferenceMessenger.Default.Send(new TodoItemUpdated(item));
        }
	}
}