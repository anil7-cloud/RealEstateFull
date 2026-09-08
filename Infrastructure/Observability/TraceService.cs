namespace REAL_ESTATE_CLEAN.Infrastructure.Observability;

public class TraceService
{
    public void Trace(string operation, int tenantId)
    {
        Console.WriteLine($"📊 TRACE → {operation} | Tenant:{tenantId} | Time:{DateTime.UtcNow}");
    }
}
