using System.IO;
using System.Text.Json;
using AshkanIndustrialOperations.Models;
namespace AshkanIndustrialOperations.Infrastructure;
public sealed class JsonUserStore : IUserStore
{
    private readonly string _path;
    private static readonly JsonSerializerOptions Options = new() { WriteIndented = true };
    public JsonUserStore()
    {
        var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AshkanIndustrialOperations");
        Directory.CreateDirectory(dir);
        _path = Path.Combine(dir, "users.json");
    }
    public async Task<IReadOnlyList<UserAccount>> GetAllAsync()
    {
        if (!File.Exists(_path)) return Array.Empty<UserAccount>();
        await using var stream = File.OpenRead(_path);
        return await JsonSerializer.DeserializeAsync<List<UserAccount>>(stream, Options) ?? new List<UserAccount>();
    }
    public async Task<UserAccount?> FindByUsernameAsync(string username) =>
        (await GetAllAsync()).FirstOrDefault(x => x.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
    public async Task SaveAsync(IReadOnlyCollection<UserAccount> users)
    {
        await using var stream = File.Create(_path);
        await JsonSerializer.SerializeAsync(stream, users, Options);
    }
}
