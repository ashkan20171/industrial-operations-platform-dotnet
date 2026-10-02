using System.IO;
using System.Text.Json;
using AshkanIndustrialOperations.Models;
namespace AshkanIndustrialOperations.Infrastructure;
public sealed class JsonAuditStore : IAuditStore
{
    private readonly string _path;
    public JsonAuditStore()
    {
        var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AshkanIndustrialOperations");
        Directory.CreateDirectory(dir);
        _path = Path.Combine(dir, "audit.jsonl");
    }
    public Task AppendAsync(AuditRecord record) => File.AppendAllTextAsync(_path, JsonSerializer.Serialize(record) + Environment.NewLine);
}
