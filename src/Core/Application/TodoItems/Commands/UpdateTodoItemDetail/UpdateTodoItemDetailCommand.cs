using MauiCleanTodos.Application.Common.Exceptions;
using MauiCleanTodos.Application.Common.Interfaces;
using MauiCleanTodos.Domain.Entities;
using MauiCleanTodos.Domain.Enums;
using MauiCleanTodos.Shared.TodoItems;
using MediatR;

namespace MauiCleanTodos.Application.TodoItems.Commands.UpdateTodoItemDetail;

public record UpdateTodoItemDetailCommand : IRequest
{
    public TodoItemDto Item { get; set; }
}

public class UpdateTodoItemDetailCommandHandler : IRequestHandler<UpdateTodoItemDetailCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateTodoItemDetailCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateTodoItemDetailCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TodoItems
            .FindAsync(new object[] { request.Item.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(TodoItem), request.Item.Id);
        }

        entity.ListId = request.Item.ListId;
        entity.Priority = (PriorityLevel)request.Item.Priority;
        entity.Note = request.Item.Note;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
