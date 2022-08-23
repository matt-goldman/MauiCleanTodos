using Maui.BindableProperty.Generator.Core;

namespace MauiCleanTodos.App.Controls;

public partial class MctEntry : ContentView
{
	[AutoBindable]
	private string _placeholder;

	public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MctEntry), string.Empty, BindingMode.TwoWay);
	public string Text
	{
		get => (string)GetValue(TextProperty);
		set
		{
			SetValue(TextProperty, value);
			OnPropertyChanged();
		}
	}

	public MctEntry()
	{
		InitializeComponent();
		BindingContext = this;
	}

	private void Entry_TextChanged(object sender, TextChangedEventArgs e)
	{
		var newText = e.NewTextValue;
		Text = newText;
    }
}