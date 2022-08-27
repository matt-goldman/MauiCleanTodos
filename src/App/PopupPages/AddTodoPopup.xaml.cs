using CommunityToolkit.Maui.Views;

namespace MauiCleanTodos.App.PopupPages;

public partial class AddTodoPopup : Popup
{
	private bool _canSave => !string.IsNullOrWhiteSpace(TitleEntry.Text);

	public AddTodoPopup()
	{
		InitializeComponent();
		BindingContext = this;
	}

	[RelayCommand]
	private void Ok()
	{
		if (_canSave)
		{
			Close(TitleEntry.Text);
		}
		else
		{
			ValidationMessage.IsVisible = true;
		}
	}
}