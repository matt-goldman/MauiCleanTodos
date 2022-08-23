using MauiCleanTodos.Application.Common.Interfaces;
using MauiCleanTodos.Domain.Entities;
using MauiCleanTodos.Domain.ValueObjects;
using MauiCleanTodos.Shared.TodoLists;
using MediatR;

namespace MauiCleanTodos.Application.TodoLists.Commands.CreateTodoList;

public record CreateTodoListCommand : IRequest<int>
{
    public string? Title { get; init; }

    public Colours Colour { get; set; } = Colours.White;
}

public class CreateTodoListCommandHandler : IRequestHandler<CreateTodoListCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateTodoListCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
    {
        var entity = new TodoList();

        entity.Title = request.Title;

        string hexColour = request.Colour.ToString("X");

        string hexColourString = $"#{hexColour.TrimStart('0')}";

        entity.Colour = Colour.From(hexColourString);

        _context.TodoLists.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
