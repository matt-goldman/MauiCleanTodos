using MauiCleanTodos.Application.TodoItems.Queries.GetTodoItemsWithPagination;
using MauiCleanTodos.Application.TodoLists.Commands.CreateTodoList;
using MauiCleanTodos.Application.TodoLists.Commands.DeleteTodoList;
using MauiCleanTodos.Application.TodoLists.Commands.UpdateTodoList;
using MauiCleanTodos.Application.TodoLists.Queries.ExportTodos;
using MauiCleanTodos.Application.TodoLists.Queries.GetTodos;
using MauiCleanTodos.Shared.Models;
using MauiCleanTodos.Shared.TodoItems;
using MauiCleanTodos.Shared.TodoLists;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MauiCleanTodos.WebUI.Controllers;

[Authorize]
public class TodoListsController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<TodosVm>> Get()
    {
        return await Mediator.Send(new GetTodosQuery());
    }

    [HttpGet("{id}")]
    public async Task<FileResult> Get(int id)
    {
        var vm = await Mediator.Send(new ExportTodosQuery { ListId = id });

        return File(vm.Content, vm.ContentType, vm.FileName);
    }

    // nswag has trouble with this
    //[HttpGet("{id}/{page}/{size}")]
    //public async Task<ActionResult<PaginatedList<TodoItemSummaryDto>>> GetTodoItemsWithPagination(int id, int page, int size)
    //{
    //    var query = new GetTodoItemsWithPaginationQuery { ListId = id, PageNumber = page, PageSize = size };
    //    return await Mediator.Send(query);
    //}

    [HttpPost]
    public async Task<ActionResult<int>> Create(string title)
    {
        var command = new CreateTodoListCommand { Title = title };
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, TodoListSummaryDto summary)
    {
        if (id != summary.Id)
        {
            return BadRequest();
        }

        var command = new UpdateTodoListCommand { Summary = summary };

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteTodoListCommand(id));

        return NoContent();
    }
}
