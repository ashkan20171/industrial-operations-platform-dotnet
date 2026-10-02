using AshkanIndustrialOperations.Models;
namespace AshkanIndustrialOperations.Infrastructure;
public interface IPlantDataStore
{
    PlantDataSnapshot Load();
    void Save(PlantDataSnapshot snapshot);
    string DatabasePath { get; }
}
