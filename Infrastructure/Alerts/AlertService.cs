namespace REAL_ESTATE_CLEAN.Infrastructure.Alerts;

public class AlertService
{
    public void Send(string message)
    {
        Console.WriteLine($"🚨 ALERT: {message}");
    }
}
