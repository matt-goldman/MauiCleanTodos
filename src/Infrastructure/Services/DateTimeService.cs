using MauiCleanTodos.Application.Common.Interfaces;

namespace MauiCleanTodos.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
