namespace REAL_ESTATE_CLEAN.Infrastructure.Services;

public class EventService
{
    public void Publish(string message)
    {
        Console.WriteLine($"EVENT: {message}");
    }
}
