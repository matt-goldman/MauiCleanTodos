using MauiCleanTodos.Application.Common.Exceptions;
using MauiCleanTodos.Application.TodoItems.Commands.CreateTodoItem;
using MauiCleanTodos.Application.TodoItems.Commands.UpdateTodoItem;
using MauiCleanTodos.Application.TodoLists.Commands.CreateTodoList;
using MauiCleanTodos.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using MauiCleanTodos.Shared.TodoItems;

namespace MauiCleanTodos.Application.IntegrationTests.TodoItems.Commands;

using static Testing;

public class UpdateTodoItemTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidTodoItemId()
    {
        var command = new UpdateTodoItemCommand
        {
            Item = new TodoItemDto
            {
                Id = 99,
                Title = "New Title"
            }
        };
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldUpdateTodoItem()
    {
        var userId = await RunAsDefaultUserAsync();

        var listId = await SendAsync(new CreateTodoListCommand
        {
            Title = "New List"
        });

        var itemId = await SendAsync(new CreateTodoItemCommand
        {
            Item = new NewTodoItemDto
            {
                ListId = listId,
                Title = "New Item"
            }
        });

        var command = new UpdateTodoItemCommand
        {
            Item = new TodoItemDto
            {
                Id = itemId,
                Title = "Updated Item Title"
            }
        };

        await SendAsync(command);

        var item = await FindAsync<TodoItem>(itemId);

        item.Should().NotBeNull();
        item!.Title.Should().Be(command.Item.Title);
        item.LastModifiedBy.Should().NotBeNull();
        item.LastModifiedBy.Should().Be(userId);
        item.LastModified.Should().NotBeNull();
        item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
