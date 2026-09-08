namespace REAL_ESTATE_CLEAN.Infrastructure.Observability;

public class Logger
{
    public void Info(string message)
    {
        Console.WriteLine($"[INFO] {DateTime.UtcNow}: {message}");
    }

    public void Error(string message)
    {
        Console.WriteLine($"[ERROR] {DateTime.UtcNow}: {message}");
    }
}
