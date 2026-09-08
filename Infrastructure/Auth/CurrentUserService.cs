using System.Security.Claims;

namespace REAL_ESTATE_CLEAN.Infrastructure.Auth;

public class CurrentUserService
{
    public int? UserId { get; private set; }

    public string? Email { get; private set; }

    public string? Role { get; private set; }

    public bool IsAuthenticated => UserId.HasValue;

    public bool IsAdmin =>
        IsAuthenticated &&
        string.Equals(Role, "Admin", StringComparison.OrdinalIgnoreCase);

    public void SetUser(int userId, string email, string role)
    {
        UserId = userId;
        Email = email;
        Role = role;
    }

    public void Clear()
    {
        UserId = null;
        Email = null;
        Role = null;
    }
}
