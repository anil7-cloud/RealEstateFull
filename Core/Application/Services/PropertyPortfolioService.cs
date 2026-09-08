using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyPortfolioService
{
    private readonly AppDbContext _context;

    public PropertyPortfolioService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PropertyPortfolio> CreatePortfolio(
        PropertyPortfolio portfolio)
    {
        portfolio.CreatedAt = DateTime.UtcNow;
        portfolio.UpdatedAt = DateTime.UtcNow;

        _context.PropertyPortfolios.Add(portfolio);

        await _context.SaveChangesAsync();

        return portfolio;
    }

    public async Task<List<PropertyPortfolio>> GetUserPortfolios(
        int ownerUserId)
    {
        return await _context.PropertyPortfolios
            .Where(x => x.OwnerUserId == ownerUserId)
            .OrderByDescending(x => x.UpdatedAt)
            .ToListAsync();
    }

    public async Task<PropertyPortfolio?> GetPortfolio(
        int id)
    {
        return await _context.PropertyPortfolios
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> UpdatePortfolio(
        PropertyPortfolio portfolio)
    {
        var existing = await _context.PropertyPortfolios
            .FirstOrDefaultAsync(x => x.Id == portfolio.Id);

        if (existing is null)
            return false;

        existing.PortfolioName = portfolio.PortfolioName;
        existing.Description = portfolio.Description;
        existing.TotalValue = portfolio.TotalValue;
        existing.PropertyCount = portfolio.PropertyCount;
        existing.IsPublic = portfolio.IsPublic;
        existing.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeletePortfolio(int id)
    {
        var portfolio = await _context.PropertyPortfolios
            .FirstOrDefaultAsync(x => x.Id == id);

        if (portfolio is null)
            return false;

        _context.PropertyPortfolios.Remove(portfolio);

        await _context.SaveChangesAsync();

        return true;
    }
}
