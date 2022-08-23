using System.Collections.ObjectModel;
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
}