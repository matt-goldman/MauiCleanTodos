using MauiCleanTodos.Shared.TodoItems;
using MauiCleanTodos.Shared.TodoLists;

namespace App.UnitTests.Mocks;
public class MockApi
{
    public List<TodoListDto> TodoLists { get; set; } = new();

    public void InitSeedData()
    {
        TodoLists.Add(new TodoListDto
        {
            Title = "Todo List",
            Id = 1,
            Items =
                {
                    new TodoItemDto { Title = "Make a todo list 📃", Id = 1 },
                    new TodoItemDto { Title = "Check off the first item ✅", Id = 2 },
                    new TodoItemDto { Title = "Realise you've already done two things on the list! 🤯", Id = 3},
                    new TodoItemDto { Title = "Reward yourself with a nice, long nap 🏆", Id = 4 },
                }
        });
    }
}
