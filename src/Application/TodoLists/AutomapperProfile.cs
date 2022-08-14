using AutoMapper;
using MauiCleanTodos.Domain.Entities;
using MauiCleanTodos.Shared.TodoItems;

namespace MauiCleanTodos.Application.TodoLists;
public class AutomapperProfile : Profile
{
	public AutomapperProfile()
	{
		CreateMap<TodoItem, TodoItemDto>()
			.ForMember(d => d.Priority, opt => opt.MapFrom(s => (int)s.Priority));
	}
}
