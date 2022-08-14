using MauiCleanTodos.Application.Common.Exceptions;
using MauiCleanTodos.Application.Common.Interfaces;
using MauiCleanTodos.Domain.Entities;
using MauiCleanTodos.Shared.TodoLists;
using MediatR;

namespace MauiCleanTodos.Application.TodoLists.Commands.UpdateTodoList;

public record UpdateTodoListCommand : IRequest
{
    public TodoListSummaryDto Summary { get; set; }
}

public class UpdateTodoListCommandHandler : IRequestHandler<UpdateTodoListCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateTodoListCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateTodoListCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TodoLists
            .FindAsync(new object[] { request.Summary.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(TodoList), request.Summary.Id);
        }

        entity.Title = request.Summary.Title;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
