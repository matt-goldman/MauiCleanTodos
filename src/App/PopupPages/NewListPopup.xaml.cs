using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Views;
using MauiCleanTodos.Shared.TodoLists;

namespace MauiCleanTodos.App.PopupPages;

public partial class NewListPopup : Popup
{
	public ObservableCollection<ColourContainer> Colours { get; set; } = new();

	public ColourContainer SelectedColour { get; set; }

	public string Title { get; set; }

	public NewListPopup()
	{
		InitializeComponent();
		BindingContext = this;
		
		foreach(var colour in (Colours[])Enum.GetValues(typeof(Colours)))
		{
			Colours.Add(new ColourContainer { Colour = colour});
		}
	}

	[RelayCommand]
	private void Ok()
	{
		var todo = new NewTodoDto
		{
			Title = Title,
			Colour = SelectedColour.Colour
		};

		Close(todo);
	}
}

public class ColourContainer
{
	public Colours Colour { get; set; }
}