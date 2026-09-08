using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyProfitabilityService
{
    private readonly AppDbContext _context;

    public PropertyProfitabilityService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<decimal> GetTotalIncome(int propertyId)
    {
        return await _context.PropertyIncomes
            .Where(x =>
                x.PropertyId == propertyId &&
                x.IsReceived)
            .SumAsync(x => x.Amount);
    }

    public async Task<decimal> GetTotalExpense(int propertyId)
    {
        return await _context.PropertyExpenses
            .Where(x =>
                x.PropertyId == propertyId &&
                x.IsPaid)
            .SumAsync(x => x.Amount);
    }

    public async Task<decimal> CalculateNetProfit(int propertyId)
    {
        var income = await GetTotalIncome(propertyId);
        var expense = await GetTotalExpense(propertyId);

        return income - expense;
    }

    public async Task<decimal> CalculateAnnualYield(
        int propertyId,
        decimal propertyValue)
    {
        if (propertyValue <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(propertyValue));
        }

        var income = await GetTotalIncome(propertyId);

        return decimal.Round(
            income / propertyValue * 100m,
            2,
            MidpointRounding.AwayFromZero);
    }

    public async Task<decimal> CalculateExpenseRatio(int propertyId)
    {
        var income = await GetTotalIncome(propertyId);

        if (income == 0)
            return 0;

        var expense = await GetTotalExpense(propertyId);

        return decimal.Round(
            expense / income * 100m,
            2,
            MidpointRounding.AwayFromZero);
    }

    public async Task<ProfitabilityResult> GetSummary(
        int propertyId,
        decimal propertyValue)
    {
        var income = await GetTotalIncome(propertyId);
        var expense = await GetTotalExpense(propertyId);
        var netProfit = income - expense;

        var annualYield = propertyValue <= 0
            ? 0
            : decimal.Round(
                income / propertyValue * 100m,
                2,
                MidpointRounding.AwayFromZero);

        var expenseRatio = income == 0
            ? 0
            : decimal.Round(
                expense / income * 100m,
                2,
                MidpointRounding.AwayFromZero);

        return new ProfitabilityResult
        {
            PropertyId = propertyId,
            TotalIncome = income,
            TotalExpense = expense,
            NetProfit = netProfit,
            AnnualYield = annualYield,
            ExpenseRatio = expenseRatio
        };
    }
}

public class ProfitabilityResult
{
    public int PropertyId { get; set; }

    public decimal TotalIncome { get; set; }

    public decimal TotalExpense { get; set; }

    public decimal NetProfit { get; set; }

    public decimal AnnualYield { get; set; }

    public decimal ExpenseRatio { get; set; }
}
