using System.Text.Json;
using AshkanIndustrialOperations.Models;
namespace AshkanIndustrialOperations.Infrastructure;
public sealed class JsonPlantDataStore : IPlantDataStore
{
    private readonly JsonSerializerOptions _json = new() { WriteIndented = true };
    public string DatabasePath { get; }
    public JsonPlantDataStore()
    {
        var root = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AshkanIndustrialOperations");
        Directory.CreateDirectory(root);
        DatabasePath = Path.Combine(root, "plant-data.v1.json");
    }
    public PlantDataSnapshot Load()
    {
        if (!File.Exists(DatabasePath)) return new();
        try { return JsonSerializer.Deserialize<PlantDataSnapshot>(File.ReadAllText(DatabasePath), _json) ?? new(); }
        catch { return new(); }
    }
    public void Save(PlantDataSnapshot snapshot)
    {
        snapshot.UpdatedAtUtc = DateTime.UtcNow;
        var temp = DatabasePath + ".tmp";
        File.WriteAllText(temp, JsonSerializer.Serialize(snapshot, _json));
        File.Move(temp, DatabasePath, true);
    }
}
