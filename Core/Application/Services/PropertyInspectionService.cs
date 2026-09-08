using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyInspectionService
{
    private readonly AppDbContext _context;

    public PropertyInspectionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PropertyInspection> CreateInspection(
        int propertyId,
        string inspectorName,
        DateTime inspectionDate,
        int structuralScore,
        int electricalScore,
        int plumbingScore,
        int roofScore,
        string findings,
        string recommendation)
    {
        structuralScore = Math.Clamp(structuralScore, 0, 100);
        electricalScore = Math.Clamp(electricalScore, 0, 100);
        plumbingScore = Math.Clamp(plumbingScore, 0, 100);
        roofScore = Math.Clamp(roofScore, 0, 100);

        var overallScore = (
            structuralScore +
            electricalScore +
            plumbingScore +
            roofScore) / 4;

        var inspection = new PropertyInspection
        {
            PropertyId = propertyId,
            InspectorName = inspectorName.Trim(),
            InspectionDate = inspectionDate,
            StructuralScore = structuralScore,
            ElectricalScore = electricalScore,
            PlumbingScore = plumbingScore,
            RoofScore = roofScore,
            OverallScore = overallScore,
            Findings = findings.Trim(),
            Recommendation = recommendation.Trim(),
            Status = "Completed",
            CreatedAt = DateTime.UtcNow
        };

        _context.PropertyInspections.Add(inspection);
        await _context.SaveChangesAsync();

        return inspection;
    }

    public async Task<PropertyInspection?> GetInspectionById(int id)
    {
        return await _context.PropertyInspections
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<PropertyInspection>> GetPropertyInspections(
        int propertyId)
    {
        return await _context.PropertyInspections
            .Where(x => x.PropertyId == propertyId)
            .OrderByDescending(x => x.InspectionDate)
            .ToListAsync();
    }

    public async Task<PropertyInspection?> GetLatestInspection(
        int propertyId)
    {
        return await _context.PropertyInspections
            .Where(x => x.PropertyId == propertyId)
            .OrderByDescending(x => x.InspectionDate)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> DeleteInspection(int id)
    {
        var inspection = await _context.PropertyInspections
            .FirstOrDefaultAsync(x => x.Id == id);

        if (inspection is null)
            return false;

        _context.PropertyInspections.Remove(inspection);
        await _context.SaveChangesAsync();

        return true;
    }
}
