using MauiCleanTodos.ApiClient.Authentication;

namespace App.UnitTests.Mocks;
public class MockAuthService : IAuthService
{
    public Task<bool> LoginAsync()
    {
        throw new NotImplementedException();
    }

    public bool Logout()
    {
        throw new NotImplementedException();
    }

    public Task<bool> RefreshLoginAsync()
    {
        throw new NotImplementedException();
    }
}
