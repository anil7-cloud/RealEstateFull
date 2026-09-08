using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class ProfileService
{
    private readonly AppDbContext _context;

    public ProfileService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<AppUser?> GetProfile(int userId)
    {
        return await _context.Users
            .FirstOrDefaultAsync(x => x.Id == userId);
    }

    public async Task<bool> UpdateProfile(
        int userId,
        string firstName,
        string lastName,
        string email,
        string phoneNumber)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Id == userId);

        if (user is null)
            return false;

        firstName = firstName.Trim();
        lastName = lastName.Trim();
        email = email.Trim();
        phoneNumber = phoneNumber.Trim();

        if (string.IsNullOrWhiteSpace(firstName) ||
            string.IsNullOrWhiteSpace(lastName) ||
            string.IsNullOrWhiteSpace(email))
        {
            return false;
        }

        var emailExists = await _context.Users
            .AnyAsync(x =>
                x.Id != userId &&
                x.Email == email);

        if (emailExists)
            return false;

        user.FirstName = firstName;
        user.LastName = lastName;
        user.Email = email;
        user.PhoneNumber = phoneNumber;

        await _context.SaveChangesAsync();

        return true;
    }
}
