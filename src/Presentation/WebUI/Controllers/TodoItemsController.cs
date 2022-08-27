using System.Net;
using MauiCleanTodos.Application.TodoItems.Commands.CreateTodoItem;
using MauiCleanTodos.Application.TodoItems.Commands.DeleteTodoItem;
using MauiCleanTodos.Application.TodoItems.Commands.UpdateTodoItem;
using MauiCleanTodos.Shared.TodoItems;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MauiCleanTodos.WebUI.Controllers;

[Authorize]
public class TodoItemsController : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<int>> Create(NewTodoItemDto item)
    {
        var command = new CreateTodoItemCommand { Item = item };
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<ActionResult> Update(int id, TodoItemDto item)
    {
        if (id != item.Id)
        {
            return BadRequest();
        }

        var command = new UpdateTodoItemCommand { Item = item };

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpPut("[action]")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<ActionResult> UpdateItemDetails(int id, TodoItemDto item)
    {
        if (id != item.Id)
        {
            return BadRequest();
        }

        var command = new UpdateTodoItemCommand { Item = item };

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteTodoItemCommand(id));

        return NoContent();
    }
}
