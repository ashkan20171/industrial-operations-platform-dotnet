using AshkanIndustrialOperations.Models;

namespace AshkanIndustrialOperations.Services;

public sealed class OperationalHealthService
{
    public IReadOnlyList<HealthFinding> Evaluate(DemoDataService data, Stage6DataService stage6)
    {
        var findings = new List<HealthFinding>();
        foreach (var machine in data.Machines)
        {
            var score = 0;
            if (machine.Status == "Alarm") score += 45;
            else if (machine.Status == "Warning") score += 25;
            if (machine.Temperature >= 75) score += 25;
            else if (machine.Temperature >= 65) score += 12;
            if (machine.Vibration >= 5) score += 30;
            else if (machine.Vibration >= 3) score += 15;
            if (machine.Oee < 75) score += 20;
            else if (machine.Oee < 85) score += 10;
            if (score >= 20)
            {
                findings.Add(new HealthFinding
                {
                    Severity = score >= 60 ? "Critical" : score >= 35 ? "High" : "Warning",
                    Area = "Asset Reliability",
                    Title = $"{machine.Name} condition requires review",
                    Detail = $"Status {machine.Status}; OEE {machine.Oee:F1}%; temperature {machine.Temperature:F0} °C; vibration {machine.Vibration:F1} mm/s.",
                    RecommendedAction = score >= 60 ? "Inspect the asset immediately and validate vibration/thermal readings." : "Trend telemetry and schedule a condition-based inspection.",
                    Score = Math.Min(score, 100)
                });
            }
        }
        foreach (var part in stage6.Parts.Where(x => x.Stock <= x.ReorderPoint))
            findings.Add(new() { Severity = part.Stock < part.ReorderPoint ? "High" : "Warning", Area = "Inventory", Title = $"{part.PartNo} reached reorder level", Detail = $"{part.Name}: {part.Stock} in stock / reorder point {part.ReorderPoint}.", RecommendedAction = "Create a replenishment request and verify lead time.", Score = part.Stock < part.ReorderPoint ? 55 : 35 });
        foreach (var plan in stage6.Plans.Where(x => x.Due <= DateTime.Today.AddDays(3)))
            findings.Add(new() { Severity = plan.Due < DateTime.Today ? "Critical" : "Warning", Area = "Maintenance", Title = $"{plan.Asset} maintenance window", Detail = $"{plan.Task} is due {plan.Due:dd MMM yyyy} ({plan.Owner}).", RecommendedAction = "Confirm resources, spare parts and production window.", Score = plan.Due < DateTime.Today ? 70 : 30 });
        return findings.OrderByDescending(x => x.Score).ThenBy(x => x.Area).ToList();
    }

    public int CalculatePlantHealth(DemoDataService data, Stage6DataService stage6)
    {
        var findings = Evaluate(data, stage6);
        var penalty = findings.Sum(x => x.Score) / Math.Max(1, data.Machines.Count + stage6.Parts.Count + stage6.Plans.Count);
        return Math.Clamp(100 - penalty, 0, 100);
    }
}
