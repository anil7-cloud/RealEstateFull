namespace REAL_ESTATE_CLEAN.Infrastructure.Security;

public class RateLimiter
{
    private static readonly Dictionary<string, int> _hits = new();

    public bool Allow(string ip)
    {
        if (!_hits.ContainsKey(ip))
            _hits[ip] = 0;

        _hits[ip]++;

        if (_hits[ip] > 100)
            return false;

        return true;
    }
}
