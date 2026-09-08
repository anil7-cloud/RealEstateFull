namespace REAL_ESTATE_CLEAN.Infrastructure.Notifications;

public class NotificationService
{
    public List<string> Notifications = new();

    public void Send(string message)
    {
        Notifications.Add(message);
    }

    public List<string> GetAll()
    {
        return Notifications;
    }
}
