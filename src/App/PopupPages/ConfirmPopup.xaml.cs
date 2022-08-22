using CommunityToolkit.Maui.Views;
using Maui.BindableProperty.Generator.Core;

namespace MauiCleanTodos.App.PopupPages;

public partial class ConfirmPopup : Popup
{
	public string ActionText { get; set; }

	public bool IsDestructive { get; set; }

	public ConfirmPopup(string actionText, bool isDestructive)
	{
		InitializeComponent();

		ActionText = actionText;
		IsDestructive = isDestructive;

		BindingContext = this;
	}

	[RelayCommand]
	private void Confirm()
	{
		Close(true);
	}

	[RelayCommand]
	private void Cancel()
	{
		Close(false);
	}
}