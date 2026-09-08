using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class NeighborhoodService
{
    private readonly AppDbContext _context;

    public NeighborhoodService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Neighborhood> CreateNeighborhood(
        string city,
        string district,
        string name,
        decimal averageSalePrice,
        decimal averageRentPrice,
        int population,
        double safetyScore,
        double transportationScore)
    {
        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException(
                "Şehir adı boş olamaz.",
                nameof(city));

        if (string.IsNullOrWhiteSpace(district))
            throw new ArgumentException(
                "İlçe adı boş olamaz.",
                nameof(district));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Mahalle adı boş olamaz.",
                nameof(name));

        var exists = await _context.Neighborhoods.AnyAsync(x =>
            x.City == city.Trim() &&
            x.District == district.Trim() &&
            x.Name == name.Trim());

        if (exists)
            throw new InvalidOperationException(
                "Bu mahalle zaten kayıtlı.");

        var neighborhood = new Neighborhood
        {
            City = city.Trim(),
            District = district.Trim(),
            Name = name.Trim(),
            AverageSalePrice = averageSalePrice,
            AverageRentPrice = averageRentPrice,
            Population = population,
            SafetyScore = safetyScore,
            TransportationScore = transportationScore,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.Neighborhoods.Add(neighborhood);
        await _context.SaveChangesAsync();

        return neighborhood;
    }

    public async Task<List<Neighborhood>> GetByCity(string city)
    {
        return await _context.Neighborhoods
            .Where(x =>
                x.City == city &&
                x.IsActive)
            .OrderBy(x => x.District)
            .ThenBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<List<Neighborhood>> GetByDistrict(
        string city,
        string district)
    {
        return await _context.Neighborhoods
            .Where(x =>
                x.City == city &&
                x.District == district &&
                x.IsActive)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<Neighborhood?> GetById(int id)
    {
        return await _context.Neighborhoods
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> UpdatePrices(
        int id,
        decimal averageSalePrice,
        decimal averageRentPrice)
    {
        var neighborhood = await _context.Neighborhoods
            .FirstOrDefaultAsync(x => x.Id == id);

        if (neighborhood is null)
            return false;

        neighborhood.AverageSalePrice = averageSalePrice;
        neighborhood.AverageRentPrice = averageRentPrice;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteNeighborhood(int id)
    {
        var neighborhood = await _context.Neighborhoods
            .FirstOrDefaultAsync(x => x.Id == id);

        if (neighborhood is null)
            return false;

        neighborhood.IsActive = false;

        await _context.SaveChangesAsync();

        return true;
    }
}
