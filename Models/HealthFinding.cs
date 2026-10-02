namespace AshkanIndustrialOperations.Models;

public sealed class HealthFinding
{
    public string Severity { get; set; } = "Info";
    public string Area { get; set; } = "Operations";
    public string Title { get; set; } = "";
    public string Detail { get; set; } = "";
    public string RecommendedAction { get; set; } = "";
    public int Score { get; set; }
}
