using MauiCleanTodos.ApiClient.Authentication;
using MauiCleanTodos.ApiClient.Services;

namespace MauiCleanTodos.App.ViewModels;
public partial class MainViewModel : BaseViewModel
{
	private readonly ITodoListsService _todoListsService;
	private readonly IAuthService _authService;

	public MainViewModel(ITodoListsService todoListsService, IAuthService authService)
	{
		_todoListsService = todoListsService;
		_authService = authService;
	}
}
