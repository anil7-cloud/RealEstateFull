using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class OpenHouseService
{
    private readonly AppDbContext _context;

    public OpenHouseService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<OpenHouse> CreateOpenHouse(
        int propertyId,
        DateTime eventDate,
        string title,
        string description)
    {
        var openHouse = new OpenHouse
        {
            PropertyId = propertyId,
            EventDate = eventDate,
            Title = title,
            Description = description,
            CreatedAt = DateTime.UtcNow
        };

        _context.OpenHouses.Add(openHouse);

        await _context.SaveChangesAsync();

        return openHouse;
    }


    public async Task<OpenHouse?> GetOpenHouseById(int id)
    {
        return await _context.OpenHouses
            .FirstOrDefaultAsync(x => x.Id == id);
    }


    public async Task<List<OpenHouse>> GetUpcomingOpenHouses()
    {
        return await _context.OpenHouses
            .Where(x =>
                x.IsActive &&
                x.EventDate >= DateTime.UtcNow)
            .OrderBy(x => x.EventDate)
            .ToListAsync();
    }


    public async Task<bool> UpdateOpenHouse(
        int id,
        DateTime eventDate,
        string title,
        string description,
        int maxParticipants)
    {
        var openHouse = await _context.OpenHouses
            .FirstOrDefaultAsync(x => x.Id == id);

        if (openHouse is null)
            return false;

        openHouse.EventDate = eventDate;
        openHouse.Title = title.Trim();
        openHouse.Description = description.Trim();
        openHouse.MaxParticipants = maxParticipants;

        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> DeleteOpenHouse(int id)
    {
        var openHouse = await _context.OpenHouses
            .FirstOrDefaultAsync(x => x.Id == id);

        if (openHouse is null)
            return false;

        _context.OpenHouses.Remove(openHouse);

        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<List<OpenHouse>> GetActiveOpenHouses()
    {
        return await _context.OpenHouses
            .Where(x => x.IsActive)
            .OrderBy(x => x.EventDate)
            .ToListAsync();
    }

}
