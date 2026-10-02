namespace AshkanIndustrialOperations.Models;
public sealed class PlantDataSnapshot
{
    public int SchemaVersion { get; set; } = 1;
    public DateTime UpdatedAtUtc { get; set; } = DateTime.UtcNow;
    public List<Machine> Machines { get; set; } = [];
    public List<Alarm> Alarms { get; set; } = [];
    public List<WorkOrder> WorkOrders { get; set; } = [];
    public List<SparePart> Parts { get; set; } = [];
    public List<MaintenancePlan> Plans { get; set; } = [];
}
