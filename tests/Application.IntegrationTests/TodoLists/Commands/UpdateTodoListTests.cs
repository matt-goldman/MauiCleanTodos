﻿using MauiCleanTodos.Application.Common.Exceptions;
using MauiCleanTodos.Application.TodoLists.Commands.CreateTodoList;
using MauiCleanTodos.Application.TodoLists.Commands.UpdateTodoList;
using MauiCleanTodos.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using MauiCleanTodos.Shared.TodoLists;

namespace MauiCleanTodos.Application.IntegrationTests.TodoLists.Commands;

using static Testing;

public class UpdateTodoListTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidTodoListId()
    {
        var command = new UpdateTodoListCommand
        {
            Summary = new TodoListSummaryDto
            {
                Id = 99,
                Title = "New Title"
            }
        };
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldRequireUniqueTitle()
    {
        var listId = await SendAsync(new CreateTodoListCommand
        {
            Title = "New List"
        });

        await SendAsync(new CreateTodoListCommand
        {
            Title = "Other List"
        });

        var command = new UpdateTodoListCommand
        {
            Summary = new TodoListSummaryDto
            {
                Id = listId,
                Title = "Other List"
            }
        };

        (await FluentActions.Invoking(() =>
            SendAsync(command))
                .Should().ThrowAsync<ValidationException>().Where(ex => ex.Errors.ContainsKey("Summary.Title")))
                .And.Errors["Summary.Title"].Should().Contain("The specified title already exists.");
    }

    [Test]
    public async Task ShouldUpdateTodoList()
    {
        var userId = await RunAsDefaultUserAsync();

        var listId = await SendAsync(new CreateTodoListCommand
        {
            Title = "New List"
        });

        var command = new UpdateTodoListCommand
        {
            Summary = new TodoListSummaryDto
            {
                Id = listId,
                Title = "Updated List Title"
            }
        };

        await SendAsync(command);

        var list = await FindAsync<TodoList>(listId);

        list.Should().NotBeNull();
        list!.Title.Should().Be(command.Summary.Title);
        list.LastModifiedBy.Should().NotBeNull();
        list.LastModifiedBy.Should().Be(userId);
        list.LastModified.Should().NotBeNull();
        list.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
