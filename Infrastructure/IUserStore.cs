using AshkanIndustrialOperations.Models;
namespace AshkanIndustrialOperations.Infrastructure;
public interface IUserStore
{
    Task<IReadOnlyList<UserAccount>> GetAllAsync();
    Task<UserAccount?> FindByUsernameAsync(string username);
    Task SaveAsync(IReadOnlyCollection<UserAccount> users);
}
