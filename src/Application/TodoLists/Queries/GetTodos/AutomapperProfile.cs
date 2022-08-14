using AutoMapper;
using MauiCleanTodos.Domain.Entities;
using MauiCleanTodos.Shared.TodoLists;

namespace MauiCleanTodos.Application.TodoLists.Queries.GetTodos;
public class AutomapperProfile : Profile
{
	public AutomapperProfile()
	{
		CreateMap<TodoList, TodoListDto>();
	}
}
