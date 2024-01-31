using App.UnitTests.Mocks;
using FluentAssertions;
using MauiCleanTodos.ApiClient.Authentication;
using MauiCleanTodos.ApiClient.Services;
using MauiCleanTodos.App.Controls;
using MauiCleanTodos.App.ViewModels;
using MauiCleanTodos.Shared.TodoItems;

namespace App.UnitTests.ViewModels;

public class MainViewModelTests
{
    private ITodoItemsService _todoItemsService;
    private ITodoListsService _todoListsService;
    private IAuthService _authService;
    private IBottomSheet _bottomSheet;

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
        _bottomSheet = new MockBottomSheet();

        _viewModel = new MainViewModel(_todoListsService, _todoItemsService, _authService, _bottomSheet);
    }

    [Test]
    public async Task UpdateTodoItem_Should_Succeed_When_ValidAsync()
    {
        // arrange
        var items = _todoListsService.GetTodos().Result
            .Select(l => l.Items)
            .FirstOrDefault();

        var item = items!.FirstOrDefault()!;

        // act
        item.Done = true;

        await _viewModel.ItemChecked(item);

        _ = _viewModel.RefreshLists();

        var itemList = _viewModel.TodoLists.FirstOrDefault(l => l.Id == item.Id);
        var itemToTest = itemList!.Items.FirstOrDefault(i => i.Id == item.Id);

        // assert
        itemToTest!.Done.Should().Be(true);
    }

    [Test]
    public void UpdateTodoItem_Should_Not_Succeed_When_Not_Valid()
    {
        //arrange
        var items = _todoListsService.GetTodos().Result
            .Select(l => l.Items)
            .FirstOrDefault();

        var item = items!.FirstOrDefault();

        // act
        item!.Done = true;
        item.ListId++;

        Func<Task> act = async () => await _viewModel.ItemChecked(item);

        // assert
        act.Should().ThrowAsync<NullReferenceException>();
    }

    
    [Test]
    public async Task AddNewTodoItem_Should_Succeed_For_Valid_ListAsync()
    {
        // arrange
        var newItemTitle = "Successful new item";

        var item = new NewTodoItemDto
        {
            ListId  = 1,
            Title   = newItemTitle
        };

        // act
        await _viewModel.AddNewItem(item);

        var list = _viewModel.TodoLists.FirstOrDefault();
        var newItems = list.Items.Where(i => i.Title == newItemTitle);

        // asert
        newItems.Count().Should().Be(1);
    }

    [Test]
    public void AddNewTodoItem_Should_Not_Succeed_For_InValid_List()
    {
        // arrange
        var newItemTitle = "Unsuccessful new item";

        var item = new NewTodoItemDto
        {
            ListId = 2,
            Title = newItemTitle
        };

        // act
        Func<Task> act = async () => { await _viewModel.AddNewItem(item); };

        // assert
        act.Should().ThrowAsync<NullReferenceException>();
    }

}
