namespace AshkanIndustrialOperations.Services;
public sealed class PermissionService
{
    private readonly SessionService _session;
    public PermissionService(SessionService session)=>_session=session;
    public bool Can(string permission)
    {
        var role=_session.CurrentUser?.Role ?? "Viewer";
        if(role=="Administrator") return true;
        return (role, permission) switch
        {
            ("Supervisor", "assets.view" or "assets.write" or "alarms.view" or "alarms.ack" or "workorders.view" or "workorders.write" or "telemetry.view" or "inventory.view" or "insights.view" or "health.view" or "connectivity.view") => true,
            ("Maintenance Engineer", "assets.view" or "alarms.view" or "alarms.ack" or "workorders.view" or "workorders.write" or "telemetry.view" or "inventory.view" or "insights.view" or "health.view") => true,
            ("Operator", "assets.view" or "alarms.view" or "alarms.ack" or "workorders.view" or "telemetry.view" or "health.view") => true,
            ("Viewer", "assets.view" or "alarms.view" or "workorders.view" or "telemetry.view" or "health.view") => true,
            _ => false
        };
    }
    public bool CanNavigate(string tag) => tag switch
    {
        "dashboard" => true, "assets" => Can("assets.view"), "alarms" => Can("alarms.view"),
        "maintenance" or "scheduler" => Can("workorders.view"), "telemetry" => Can("telemetry.view"),
        "inventory" => Can("inventory.view"), "insights" => Can("insights.view"), "health" => Can("health.view"),
        "connectivity" => Can("connectivity.view"), "rules" => Can("alarms.ack"),
        "portfolio" => true,
        "users" or "audit" or "settings" => (_session.CurrentUser?.Role ?? "Viewer") == "Administrator",
        _ => false
    };
}
