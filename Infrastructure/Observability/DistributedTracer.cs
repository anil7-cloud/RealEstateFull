namespace REAL_ESTATE_CLEAN.Infrastructure.Observability;

public class DistributedTracer
{
    public void Trace(string service, string action)
    {
        var traceId = Guid.NewGuid().ToString();

        Console.WriteLine($"🔎 TRACE [{traceId}] {service} → {action}");
    }
}
