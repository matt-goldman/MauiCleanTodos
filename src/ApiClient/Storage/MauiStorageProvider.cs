namespace MauiCleanTodos.ApiClient.Storage;
public class MauiStorageProvider : ISecureStorageProvider
{
    public async Task<string> GetAsync(string name)
    {
        return await SecureStorage.GetAsync(name);
    }

    public void Remove(string name)
    {
        SecureStorage.Remove(name);
    }

    public async Task SaveAsync(string name, string value)
    {
        await SecureStorage.SetAsync(name, value);
    }
}
