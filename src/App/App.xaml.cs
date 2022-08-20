namespace MauiCleanTodos.App;

public partial class App : Application
{
	public App(MainViewModel viewModel)
	{
		InitializeComponent();

		MainPage = new MainPage(viewModel);
	}
}
