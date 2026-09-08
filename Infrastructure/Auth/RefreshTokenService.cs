namespace REAL_ESTATE_CLEAN.Infrastructure.Auth;

public class RefreshTokenService
{
    private readonly Dictionary<string, string> _store = new();

    public string Create(string userId)
    {
        var token = Guid.NewGuid().ToString();
        _store[token] = userId;
        return token;
    }
}
