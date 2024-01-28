using System.Globalization;
using MauiCleanTodos.Application.TodoLists.Queries.ExportTodos;
using CsvHelper.Configuration;

namespace MauiCleanTodos.Infrastructure.Files.Maps;

public class TodoItemRecordMap : ClassMap<TodoItemRecord>
{
    public TodoItemRecordMap()
    {
        AutoMap(CultureInfo.InvariantCulture);

        Map(m => m.Done).Convert(args => args.Value.Done ? "Yes" : "No");
    }
}

