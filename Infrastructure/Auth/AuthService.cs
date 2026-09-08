using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using REAL_ESTATE_CLEAN.Core.Persistence;
using REAL_ESTATE_CLEAN.Core.Application.Services;

namespace REAL_ESTATE_CLEAN.Infrastructure.Auth;

public class AuthService
{
    private readonly AppDbContext _db;
    private readonly IConfiguration _config;
    private readonly SpamProtectionService _spamProtectionService;

    public AuthService(
        AppDbContext db,
        IConfiguration config,
        SpamProtectionService spamProtectionService)
    {
        _db = db;
        _config = config;
        _spamProtectionService = spamProtectionService;
    }

    public async Task<bool> Register(
        string email,
        string password)
    {
        Console.WriteLine($"REGISTER CALLED: {email}");

        email = email.Trim();

        var exists = await _db.Users
            .AnyAsync(x => x.Email == email);

        if (exists)
            return false;

        var user = new AppUser
        {
            FirstName = "Yeni",
            LastName = "Kullanıcı",
            Email = email,
            PhoneNumber = "",
            PasswordHash = password,
            Role = "User",
            CreatedAt = DateTime.UtcNow
        };

        _db.Users.Add(user);

        await _db.SaveChangesAsync();

        return true;
    }


    public async Task<AppUser?> GetUserForLogin(
        string email,
        string password)
    {
        email = email.Trim();

        var user = await _db.Users
            .FirstOrDefaultAsync(x => x.Email == email);

        if (user == null)
            return null;

        if (user.PasswordHash != password)
            return null;

        return user;
    }

    public async Task<string?> LoginWithJwt(
        string email,
        string password)
    {
        var user = await _db.Users
            .FirstOrDefaultAsync(x => x.Email == email);

        if (user == null)
            return null;

        if (user.PasswordHash != password)
            return null;

        return GenerateToken(user);
    }

    public string GenerateToken(AppUser user)
    {
        var jwtKey = _config["Jwt:Key"];

        if (string.IsNullOrWhiteSpace(jwtKey))
            throw new InvalidOperationException(
                "Jwt:Key configuration is missing.");

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtKey));

        var creds = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(
                ClaimTypes.NameIdentifier,
                user.Id.ToString()),

            new Claim(
                ClaimTypes.Email,
                user.Email),

            new Claim(
                ClaimTypes.Role,
                user.Role)
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }
}
