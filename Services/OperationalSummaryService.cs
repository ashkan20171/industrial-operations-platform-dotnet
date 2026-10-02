namespace AshkanIndustrialOperations.Services;

public sealed record OperationalSummary(
    double AverageOee,
    int HealthyAssets,
    int TotalAssets,
    int AttentionAssets,
    int ActiveAlarms,
    int CriticalAlarms,
    int OpenWorkOrders,
    int LowStockParts,
    int DueMaintenance,
    int RiskScore,
    string RiskLabel);

public sealed class OperationalSummaryService
{
    public OperationalSummary Build(DemoDataService data, Stage6DataService stage6)
    {
        var avg = data.Machines.Count == 0 ? 0 : data.Machines.Average(x => x.Oee);
        var attention = data.Machines.Count(x => x.Status is "Alarm" or "Warning");
        var active = data.Alarms.Count(x => x.State != "Acknowledged");
        var critical = data.Alarms.Count(x => x.Severity == "Critical" && x.State != "Acknowledged");
        var open = data.WorkOrders.Count(x => x.Status != "Completed");
        var lowStock = stage6.Parts.Count(x => x.Stock <= x.ReorderPoint);
        var due = stage6.Plans.Count(x => x.Due <= DateTime.Today.AddDays(7));
        var risk = Math.Clamp(critical * 25 + attention * 12 + lowStock * 8 + due * 4, 0, 100);
        var label = risk >= 70 ? "Critical attention" : risk >= 40 ? "Elevated risk" : "Controlled risk";
        return new(avg, data.Machines.Count - attention, data.Machines.Count, attention, active, critical, open, lowStock, due, risk, label);
    }
}
