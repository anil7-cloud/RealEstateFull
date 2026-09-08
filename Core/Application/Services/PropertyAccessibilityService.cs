using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyAccessibilityService
{
    private readonly AppDbContext _context;

    public PropertyAccessibilityService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<PropertyAccessibility> CreateAccessibility(
        Guid propertyId,
        bool hasWheelchairRamp,
        bool hasAccessibleElevator,
        bool hasWideDoors,
        bool hasAccessibleBathroom,
        bool hasAccessibleParking,
        bool hasStepFreeEntrance,
        string notes)
    {
        var exists = await _context.PropertyAccessibilities
            .AnyAsync(x => x.PropertyListingId == propertyId);

        if (exists)
            throw new InvalidOperationException(
                "Bu ilan için erişilebilirlik kaydı zaten mevcut.");


        var accessibility = new PropertyAccessibility
        {
            PropertyListingId = propertyId,
            HasWheelchairRamp = hasWheelchairRamp,
            HasAccessibleElevator = hasAccessibleElevator,
            HasWideDoors = hasWideDoors,
            HasAccessibleBathroom = hasAccessibleBathroom,
            HasAccessibleParking = hasAccessibleParking,
            HasStepFreeEntrance = hasStepFreeEntrance,
            Notes = notes.Trim(),
            CreatedAt = DateTime.UtcNow
        };


        _context.PropertyAccessibilities.Add(accessibility);

        await _context.SaveChangesAsync();

        return accessibility;
    }


    public async Task<PropertyAccessibility?> GetByPropertyId(Guid propertyId)
    {
        return await _context.PropertyAccessibilities
            .FirstOrDefaultAsync(x =>
                x.PropertyListingId == propertyId);
    }


    public async Task<bool> UpdateAccessibility(
        Guid propertyId,
        bool hasWheelchairRamp,
        bool hasAccessibleElevator,
        bool hasWideDoors,
        bool hasAccessibleBathroom,
        bool hasAccessibleParking,
        bool hasStepFreeEntrance,
        string notes)
    {
        var accessibility = await _context.PropertyAccessibilities
            .FirstOrDefaultAsync(x =>
                x.PropertyListingId == propertyId);

        if (accessibility == null)
            return false;


        accessibility.HasWheelchairRamp = hasWheelchairRamp;
        accessibility.HasAccessibleElevator = hasAccessibleElevator;
        accessibility.HasWideDoors = hasWideDoors;
        accessibility.HasAccessibleBathroom = hasAccessibleBathroom;
        accessibility.HasAccessibleParking = hasAccessibleParking;
        accessibility.HasStepFreeEntrance = hasStepFreeEntrance;
        accessibility.Notes = notes.Trim();
        accessibility.UpdatedAt = DateTime.UtcNow;


        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> DeleteAccessibility(Guid id)
    {
        var accessibility = await _context.PropertyAccessibilities
            .FirstOrDefaultAsync(x => x.Id == id);

        if (accessibility == null)
            return false;


        _context.PropertyAccessibilities.Remove(accessibility);

        await _context.SaveChangesAsync();

        return true;
    }
}
