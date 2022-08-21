using Microsoft.Extensions.Options;

namespace MauiCleanTodos.App;

public partial class MainPage : ContentPage
{
	private readonly MainViewModel _viewModel;

	public MainPage(MainViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		_viewModel.Navigation = Navigation;
		BindingContext = _viewModel;
		_viewModel.Parent = this;
	}
}

