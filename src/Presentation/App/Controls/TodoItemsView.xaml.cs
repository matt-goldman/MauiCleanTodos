namespace MauiCleanTodos.App.Controls;

public partial class TodoItemsView : ContentView
{
	public TodoItemsView()
	{
		InitializeComponent();

		BusyIndicator.IsVisible = false;
	}

	protected override void OnSizeAllocated(double width, double height)
	{
		base.OnSizeAllocated(width, height);
	}
}