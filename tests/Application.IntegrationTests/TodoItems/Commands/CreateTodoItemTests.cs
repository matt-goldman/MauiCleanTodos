using FluentAssertions;
using MauiCleanTodos.Application.Common.Exceptions;
using MauiCleanTodos.Application.TodoItems.Commands.CreateTodoItem;
using MauiCleanTodos.Application.TodoLists.Commands.CreateTodoList;
using MauiCleanTodos.Domain.Entities;
using MauiCleanTodos.Shared.TodoItems;
using NUnit.Framework;

namespace MauiCleanTodos.Application.IntegrationTests.TodoItems.Commands;

using static Testing;

public class CreateTodoItemTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateTodoItemCommand
        {
            Item = new NewTodoItemDto()
        };

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldRequireTodoItem()
    {
        var command = new CreateTodoItemCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<NullReferenceException>();
    }

    [Test]
    public async Task ShouldCreateTodoItem()
    {
        var userId = await RunAsDefaultUserAsync();

        var listId = await SendAsync(new CreateTodoListCommand
        {
            Title = "New List"
        });

        var command = new CreateTodoItemCommand
        {
            Item = new NewTodoItemDto
            {
                ListId = listId,
                Title = "Tasks"
            }
        };

        var itemId = await SendAsync(command);

        var item = await FindAsync<TodoItem>(itemId);

        item.Should().NotBeNull();
        item!.ListId.Should().Be(command.Item.ListId);
        item.Title.Should().Be(command.Item.Title);
        item.CreatedBy.Should().Be(userId);
        item.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
        item.LastModifiedBy.Should().Be(userId);
        item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
