using AutoMapper;
using MauiCleanTodos.Domain.Entities;
using MauiCleanTodos.Shared.TodoItems;

namespace MauiCleanTodos.Application.TodoItems.Queries.GetTodoItemsWithPagination;
public class AutomapperProfile : Profile
{
	public AutomapperProfile()
	{
		CreateMap<TodoItem, TodoItemSummaryDto>();
	}
}
