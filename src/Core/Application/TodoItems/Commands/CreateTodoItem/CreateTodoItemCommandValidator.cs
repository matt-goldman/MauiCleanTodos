using FluentValidation;

namespace MauiCleanTodos.Application.TodoItems.Commands.CreateTodoItem;

public class CreateTodoItemCommandValidator : AbstractValidator<CreateTodoItemCommand>
{
    public CreateTodoItemCommandValidator()
    {
        RuleFor(v => v.Item).NotNull();

        RuleFor(v => v.Item.Title)
            .MaximumLength(200)
            .NotEmpty();
    }
}
