using App.UnitTests.Mocks;
using FluentAssertions;
using MauiCleanTodos.ApiClient.Authentication;
using MauiCleanTodos.ApiClient.Services;
using MauiCleanTodos.App.ViewModels;

namespace App.UnitTests.ViewModels;

public class MainViewModelTests
{
    private ITodoItemsService _todoItemsService;
    private ITodoListsService _todoListsService;
    private IAuthService _authService;

    private MainViewModel _viewModel;

    private MockApi _api;

    [SetUp]
    public void Setup()
    {
        _api = new MockApi();

        _api.InitSeedData();

        _todoItemsService = new MockTodoItemsService(_api);
        _todoListsService = new MockTodoListsService(_api);
        _authService = new MockAuthService();

        _viewModel = new MainViewModel(_todoListsService, _todoItemsService, _authService);
    }

    [Test]
    public void UpdateTodoItem_Should_Succeed_When_Valid()
    {
        //arrange
        var items = _todoListsService.GetTodos().Result
            .Select(l => l.Items)
            .FirstOrDefault();

        var item = items.FirstOrDefault();

        // act
        item.Done = true;

        _viewModel.UpdateTodoItem(item);

        // assert
        _ = _viewModel.RefreshLists();

        var itemList = _viewModel.TodoLists.FirstOrDefault(l => l.Id == item.Id);
        var itemToTest = itemList.Items.FirstOrDefault(i => i.Id == item.Id);

        itemToTest.Done.Should().Be(true);
    }

    [Test]
    public void UpdateTodoItem_Should_Not_Succeed_When_Not_Valid()
    {
        // arrange

        //act

        // asert
    }

    
    [Test]
    public void AddNewTodoItem_Should_Succeed_For_Valid_List()
    {
        // arrange

        //act

        // asert
    }

    [Test]
    public void AddNewTodoItem_Should_Not_Succeed_For_InValid_List()
    {
        // arrange

        //act

        // asert
    }

    [Test]
    public void RefreshLists_Should_Include_All_Lists_In_Service()
    {
        // arrange

        //act

        // asert
    }

}
