namespace AshkanIndustrialOperations.Services;
public sealed record NavigationItem(string Tag,string Title,string Capability);
public static class NavigationCatalog
{
    public static IReadOnlyList<NavigationItem> Items { get; } = new[]
    {
        new NavigationItem("dashboard","Command Center","Plant overview"),new("assets","Asset Management","Asset registry & condition"),new("alarms","Alarm Center","Alarm lifecycle"),
        new("maintenance","Maintenance & Work Orders","Execution workflow"),new("telemetry","Live Telemetry","Streaming condition data"),new("inventory","Spare Parts & Inventory","MRO stock control"),
        new("rules","Alarm Rules Engine","Threshold governance"),new("scheduler","Maintenance Scheduler","Preventive planning"),new("insights","Reliability Insights","Explainable risk scoring"),
        new("health","Operational Health Center","Cross-domain health"),new("connectivity","Industrial Connectivity","OPC UA / MQTT adapters"),new("users","Users & Roles","RBAC administration"),
        new("audit","Audit Trail","Traceability"),new("portfolio","Engineering Overview","Architecture & portfolio"),new("settings","Settings","Application preferences")
    };
    public static string TitleFor(string tag)=>Items.FirstOrDefault(x=>x.Tag==tag)?.Title ?? "Command Center";
}
