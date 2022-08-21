using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Views;
using MauiCleanTodos.Shared.TodoLists;

namespace MauiCleanTodos.App.PopupPages;

public partial class NewListPopup : Popup
{
	public ObservableCollection<test> Colours { get; set; } = new();

	public NewListPopup()
	{
		InitializeComponent();
		BindingContext = this;
		
		foreach(var colour in (Colours[])Enum.GetValues(typeof(Colours)))
		{
			Colours.Add(new test { Colour = colour});
		}
	}

	private void ColoursSelectionChanged(object sender, SelectionChangedEventArgs e)
	{

    }
}

public class test
{
	public Colours Colour { get; set; }
}