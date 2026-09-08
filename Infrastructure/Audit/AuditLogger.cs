namespace REAL_ESTATE_CLEAN.Infrastructure.Audit;

public class AuditLogger
{
    public void Log(string action, int tenantId, string details)
    {
        Console.WriteLine($"[AUDIT] {DateTime.UtcNow} | Tenant:{tenantId} | {action} | {details}");
    }
}
