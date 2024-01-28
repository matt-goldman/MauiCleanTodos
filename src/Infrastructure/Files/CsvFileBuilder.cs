using System.Globalization;
using MauiCleanTodos.Application.Common.Interfaces;
using MauiCleanTodos.Application.TodoLists.Queries.ExportTodos;
using MauiCleanTodos.Infrastructure.Files.Maps;
using CsvHelper;

namespace MauiCleanTodos.Infrastructure.Files;

public class CsvFileBuilder : ICsvFileBuilder
{
    public byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records)
    {
        using var memoryStream = new MemoryStream();
        using (var streamWriter = new StreamWriter(memoryStream))
        {
            using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

            csvWriter.Context.RegisterClassMap<TodoItemRecordMap>();
            csvWriter.WriteRecords(records);
        }

        return memoryStream.ToArray();
    }
}
