using MauiCleanTodos.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace MauiCleanTodos.Application.Common.Extensions;
public static class QueryableExtensions
{
    public static async Task<PaginatedList<T>> ToPaginatedList<T>(this IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PaginatedList<T>(items, count, pageNumber, pageSize);
    }
}
