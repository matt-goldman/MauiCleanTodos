using MauiCleanTodos.ApiClient.Storage;

namespace BlazorApiClient.Storage;

public class BlazorStorageProvider : ISecureStorageProvider
{
    public Task<string> GetAsync(string name)
    {
        throw new NotImplementedException();
    }

    public void Remove(string name)
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync(string name, string value)
    {
        throw new NotImplementedException();
    }
}
