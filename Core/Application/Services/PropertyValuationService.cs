using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyValuationService
{
    private readonly AppDbContext _context;

    public PropertyValuationService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PropertyValuation> CreateValuation(
        int propertyId,
        decimal areaSquareMeters,
        int roomCount,
        int buildingAge,
        double locationScore,
        decimal averageSquareMeterPrice)
    {
        if (areaSquareMeters <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(areaSquareMeters));

        if (roomCount <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(roomCount));

        if (buildingAge < 0)
            throw new ArgumentOutOfRangeException(
                nameof(buildingAge));

        if (locationScore < 0 || locationScore > 10)
            throw new ArgumentOutOfRangeException(
                nameof(locationScore));

        if (averageSquareMeterPrice <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(averageSquareMeterPrice));

        var baseValue =
            areaSquareMeters * averageSquareMeterPrice;

        var roomMultiplier =
            1m + Math.Min(roomCount, 10) * 0.02m;

        var ageMultiplier =
            Math.Max(0.65m, 1m - buildingAge * 0.01m);

        var locationMultiplier =
            0.80m + (decimal)locationScore * 0.04m;

        var estimatedValue =
            baseValue *
            roomMultiplier *
            ageMultiplier *
            locationMultiplier;

        var valuation = new PropertyValuation
        {
            PropertyId = propertyId,
            AreaSquareMeters = areaSquareMeters,
            RoomCount = roomCount,
            BuildingAge = buildingAge,
            LocationScore = locationScore,
            AverageSquareMeterPrice = averageSquareMeterPrice,
            EstimatedValue = Math.Round(estimatedValue, 2),
            Currency = "TRY",
            ValuationDate = DateTime.UtcNow
        };

        _context.PropertyValuations.Add(valuation);
        await _context.SaveChangesAsync();

        return valuation;
    }

    public async Task<List<PropertyValuation>> GetPropertyValuations(
        int propertyId)
    {
        return await _context.PropertyValuations
            .Where(x => x.PropertyId == propertyId)
            .OrderByDescending(x => x.ValuationDate)
            .ToListAsync();
    }

    public async Task<PropertyValuation?> GetLatestValuation(
        int propertyId)
    {
        return await _context.PropertyValuations
            .Where(x => x.PropertyId == propertyId)
            .OrderByDescending(x => x.ValuationDate)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> DeleteValuation(int id)
    {
        var valuation = await _context.PropertyValuations
            .FirstOrDefaultAsync(x => x.Id == id);

        if (valuation is null)
            return false;

        _context.PropertyValuations.Remove(valuation);
        await _context.SaveChangesAsync();

        return true;
    }
}
