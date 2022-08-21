using Microsoft.Extensions.Options;

namespace MauiCleanTodos.ApiClient.Services;
public interface ITodoListsService
{
    Task<List<TodoListDto>> GetTodos();

    List<PriorityLevelDto> PriorityLevels { get; }
}

public class TodoListsService : BaseService, ITodoListsService
{
    private readonly TodoListsClient _client;

    private List<PriorityLevelDto> _priorityLevels = new();

    public TodoListsService(IHttpClientFactory httpClientFactory, ApiClientOptions options) : base(httpClientFactory, options)
    {
        _client = new TodoListsClient(options.BaseUrl, _httpClient);
    }

    public List<PriorityLevelDto> PriorityLevels { get => _priorityLevels; }

    public async Task<List<TodoListDto>> GetTodos()
    {
        var vm = await _client.GetAsync();

        _priorityLevels = vm.PriorityLevels.ToList();

        return vm.Lists.ToList();
    }
}
