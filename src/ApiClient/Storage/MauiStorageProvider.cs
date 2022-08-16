namespace MauiCleanTodos.ApiClient.Storage;
public class MauiStorageProvider : ISecureStorageProvider
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
