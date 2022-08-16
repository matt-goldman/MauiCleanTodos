using Microsoft.Extensions.Options;

namespace MauiCleanTodos.ApiClient.Services;

public interface ITodoItemsService
{

}

public class TodoItemsService : BaseService, ITodoItemsService
{
    public TodoItemsService(IHttpClientFactory httpClientFactory, IOptions<ApiClientOptions> options) : base(httpClientFactory, options)
    {
    }
}
