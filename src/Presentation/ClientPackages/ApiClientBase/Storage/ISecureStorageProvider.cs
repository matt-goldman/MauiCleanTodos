namespace MauiCleanTodos.ApiClient.Storage;
public interface ISecureStorageProvider
{
    Task<string> GetAsync(string name);

    Task SaveAsync(string name, string value);

    void Remove(string name);
}
