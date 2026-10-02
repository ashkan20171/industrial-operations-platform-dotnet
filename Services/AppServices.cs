using AshkanIndustrialOperations.Infrastructure;
using AshkanIndustrialOperations.Security;
namespace AshkanIndustrialOperations.Services;
public static class AppServices
{
    public static SessionService Session { get; } = new();
    public static PasswordHasher PasswordHasher { get; } = new();
    public static IUserStore Users { get; } = new JsonUserStore();
    public static IAuditStore Audit { get; } = new JsonAuditStore();
    public static PermissionService Permissions { get; } = new(Session);
    public static IndustrialConnectivityService Connectivity { get; } = new();
    public static IPlantDataStore PlantStore { get; } = new JsonPlantDataStore();
    public static PlantPersistenceService Persistence { get; } = new(PlantStore);
    public static OperationalSummaryService OperationalSummary { get; } = new();
    public static AuthenticationService Authentication { get; } = new(Users, Audit, PasswordHasher, Session);
}
