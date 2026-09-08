using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class SchoolService
{
    private readonly AppDbContext _context;

    public SchoolService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<School> CreateSchool(
        int propertyId,
        string name,
        string type,
        decimal distanceKm,
        string address,
        double rating)
    {
        var school = new School
        {
            PropertyId = propertyId,
            Name = name.Trim(),
            Type = type.Trim(),
            DistanceKm = distanceKm,
            Address = address.Trim(),
            Rating = rating,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.Schools.Add(school);

        await _context.SaveChangesAsync();

        return school;
    }

    public async Task<List<School>> GetSchools(int propertyId)
    {
        return await _context.Schools
            .Where(x =>
                x.PropertyId == propertyId &&
                x.IsActive)
            .OrderBy(x => x.DistanceKm)
            .ToListAsync();
    }

    public async Task<bool> DeleteSchool(int id)
    {
        var school = await _context.Schools
            .FirstOrDefaultAsync(x => x.Id == id);

        if (school is null)
            return false;

        _context.Schools.Remove(school);

        await _context.SaveChangesAsync();

        return true;
    }
}
