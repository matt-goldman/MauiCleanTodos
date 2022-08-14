using MauiCleanTodos.Shared.PriorityLevels;

namespace MauiCleanTodos.Shared.TodoLists;
public class TodosVm
{
    public IList<PriorityLevelDto> PriorityLevels { get; set; } = new List<PriorityLevelDto>();

    public IList<TodoListDto> Lists { get; set; } = new List<TodoListDto>();
}
