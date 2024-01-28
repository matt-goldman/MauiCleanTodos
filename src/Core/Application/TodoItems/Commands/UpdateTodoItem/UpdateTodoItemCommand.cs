using MauiCleanTodos.Application.Common.Exceptions;
using MauiCleanTodos.Application.Common.Interfaces;
using MauiCleanTodos.Domain.Entities;
using MauiCleanTodos.Shared.TodoItems;
using MediatR;

namespace MauiCleanTodos.Application.TodoItems.Commands.UpdateTodoItem;

public record UpdateTodoItemCommand : IRequest
{
    public TodoItemDto Item { get; set; }
}

public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateTodoItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TodoItems
            .FindAsync([request.Item.Id], cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(TodoItem), request.Item.Id);
        }

        entity.Title = request.Item.Title;
        entity.Done = request.Item.Done;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
