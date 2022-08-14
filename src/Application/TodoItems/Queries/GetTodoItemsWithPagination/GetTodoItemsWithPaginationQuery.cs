using AutoMapper;
using AutoMapper.QueryableExtensions;
using MauiCleanTodos.Application.Common.Extensions;
using MauiCleanTodos.Application.Common.Interfaces;
using MauiCleanTodos.Shared.Models;
using MauiCleanTodos.Shared.TodoItems;
using MediatR;

namespace MauiCleanTodos.Application.TodoItems.Queries.GetTodoItemsWithPagination;

public record GetTodoItemsWithPaginationQuery : IRequest<PaginatedList<TodoItemSummaryDto>>
{
    public int ListId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetTodoItemsWithPaginationQueryHandler : IRequestHandler<GetTodoItemsWithPaginationQuery, PaginatedList<TodoItemSummaryDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTodoItemsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<TodoItemSummaryDto>> Handle(GetTodoItemsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.TodoItems
            .Where(x => x.ListId == request.ListId)
            .OrderBy(x => x.Title)
            .ProjectTo<TodoItemSummaryDto>(_mapper.ConfigurationProvider)
            .ToPaginatedList(request.PageNumber, request.PageSize);
    }
}
