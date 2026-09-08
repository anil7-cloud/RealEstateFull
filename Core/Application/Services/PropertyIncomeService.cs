using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyIncomeService
{
    private readonly AppDbContext _context;

    public PropertyIncomeService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PropertyIncome> CreateIncome(
        PropertyIncome income)
    {
        _context.PropertyIncomes.Add(income);

        await _context.SaveChangesAsync();

        return income;
    }

    public async Task<List<PropertyIncome>> GetPropertyIncomes(
        int propertyId)
    {
        return await _context.PropertyIncomes
            .Where(x => x.PropertyId == propertyId)
            .OrderByDescending(x => x.IncomeDate)
            .ToListAsync();
    }

    public async Task<decimal> GetTotalIncome(
        int propertyId)
    {
        return await _context.PropertyIncomes
            .Where(x =>
                x.PropertyId == propertyId &&
                x.IsReceived)
            .SumAsync(x => x.Amount);
    }

    public async Task<bool> MarkAsReceived(int id)
    {
        var income = await _context.PropertyIncomes
            .FirstOrDefaultAsync(x => x.Id == id);

        if (income is null)
            return false;

        income.IsReceived = true;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteIncome(int id)
    {
        var income = await _context.PropertyIncomes
            .FirstOrDefaultAsync(x => x.Id == id);

        if (income is null)
            return false;

        _context.PropertyIncomes.Remove(income);

        await _context.SaveChangesAsync();

        return true;
    }
}
