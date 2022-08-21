using Maui.BindableProperty.Generator.Core;

namespace MauiCleanTodos.App.Controls;

public partial class MctEntry : ContentView
{
	[AutoBindable]
	private string _placeholder;

	[AutoBindable]
	private string _text;

	public MctEntry()
	{
		InitializeComponent();
		BindingContext = this;
	}
}