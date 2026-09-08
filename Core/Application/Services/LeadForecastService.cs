using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadForecastService
{
    private readonly AppDbContext _context;

    public LeadForecastService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<LeadForecast> CreateAsync(LeadForecast forecast)
    {
        forecast.CreatedAt = DateTime.UtcNow;
        forecast.IsActive = true;

        _context.LeadForecasts.Add(forecast);

        await _context.SaveChangesAsync();

        return forecast;
    }


    public async Task<List<LeadForecast>> GetAllAsync()
    {
        return await _context.LeadForecasts
            .Where(x => x.IsActive)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<LeadForecast?> GetByIdAsync(int id)
    {
        return await _context.LeadForecasts
            .FirstOrDefaultAsync(x => x.Id == id);
    }


    public async Task<List<LeadForecast>> GetByLeadAsync(int leadId)
    {
        return await _context.LeadForecasts
            .Where(x => x.LeadId == leadId && x.IsActive)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<LeadForecast?> GetLatestAsync(int leadId)
    {
        return await _context.LeadForecasts
            .Where(x => x.LeadId == leadId && x.IsActive)
            .OrderByDescending(x => x.CreatedAt)
            .FirstOrDefaultAsync();
    }


    public async Task<bool> UpdateAsync(LeadForecast updated)
    {
        var forecast = await _context.LeadForecasts
            .FirstOrDefaultAsync(x => x.Id == updated.Id);

        if (forecast == null)
            return false;


        forecast.EstimatedSalePrice = updated.EstimatedSalePrice;
        forecast.EstimatedCommission = updated.EstimatedCommission;
        forecast.Probability = updated.Probability;
        forecast.ExpectedRevenue = updated.ExpectedRevenue;
        forecast.ExpectedClosingDate = updated.ExpectedClosingDate;
        forecast.ForecastStatus = updated.ForecastStatus;
        forecast.RiskLevel = updated.RiskLevel;
        forecast.Recommendation = updated.Recommendation;
        forecast.Notes = updated.Notes;

        forecast.UpdatedAt = DateTime.UtcNow;


        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> DeleteAsync(int id)
    {
        var forecast = await _context.LeadForecasts
            .FirstOrDefaultAsync(x => x.Id == id);

        if (forecast == null)
            return false;


        forecast.IsActive = false;
        forecast.UpdatedAt = DateTime.UtcNow;


        await _context.SaveChangesAsync();

        return true;
    }
}
