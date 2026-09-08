using System.Collections.Concurrent;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class SpamProtectionService
{
    private readonly ConcurrentDictionary<string, List<DateTime>> _requests = new();

    private readonly TimeSpan _window = TimeSpan.FromMinutes(1);

    public bool IsAllowed(
        string key,
        int maxRequests = 10)
    {
        if (string.IsNullOrWhiteSpace(key))
            return false;

        var now = DateTime.UtcNow;

        var history = _requests.GetOrAdd(
            key.Trim().ToLowerInvariant(),
            _ => new List<DateTime>());

        lock (history)
        {
            history.RemoveAll(x =>
                now - x > _window);

            if (history.Count >= maxRequests)
                return false;

            history.Add(now);

            return true;
        }
    }

    public bool IsRegistrationAllowed(string email)
    {
        return IsAllowed(
            $"register:{email}",
            3);
    }

    public bool IsPropertyCreationAllowed(int userId)
    {
        return IsAllowed(
            $"property:{userId}",
            10);
    }

    public bool IsMessageAllowed(int userId)
    {
        return IsAllowed(
            $"message:{userId}",
            20);
    }

    public bool IsReportAllowed(int userId)
    {
        return IsAllowed(
            $"report:{userId}",
            5);
    }

    public bool IsSearchAllowed(int userId)
    {
        return IsAllowed(
            $"search:{userId}",
            60);
    }

    public void Clear(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            return;

        _requests.TryRemove(
            key.Trim().ToLowerInvariant(),
            out _);
    }
}
