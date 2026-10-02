using AshkanIndustrialOperations.Models;
namespace AshkanIndustrialOperations.Infrastructure;
public interface IAuditStore { Task AppendAsync(AuditRecord record); }
