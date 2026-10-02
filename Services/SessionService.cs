using AshkanIndustrialOperations.Models;
namespace AshkanIndustrialOperations.Services;
public sealed class SessionService
{
    public UserAccount? CurrentUser { get; private set; }
    public bool IsAuthenticated => CurrentUser is not null;
    public void SignIn(UserAccount user) => CurrentUser = user;
    public void SignOut() => CurrentUser = null;
}
