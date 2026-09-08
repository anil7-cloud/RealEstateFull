using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyCashFlowService
{
    private readonly AppDbContext _context;

    public PropertyCashFlowService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<MonthlyCashFlowResult>> GetMonthlyCashFlow(
        int propertyId,
        int year)
    {
        var incomes = await _context.PropertyIncomes
            .Where(x =>
                x.PropertyId == propertyId &&
                x.IsReceived &&
                x.IncomeDate.Year == year)
            .GroupBy(x => x.IncomeDate.Month)
            .Select(g => new
            {
                Month = g.Key,
                Total = g.Sum(x => x.Amount)
            })
            .ToListAsync();

        var expenses = await _context.PropertyExpenses
            .Where(x =>
                x.PropertyId == propertyId &&
                x.IsPaid &&
                x.ExpenseDate.Year == year)
            .GroupBy(x => x.ExpenseDate.Month)
            .Select(g => new
            {
                Month = g.Key,
                Total = g.Sum(x => x.Amount)
            })
            .ToListAsync();

        var result = new List<MonthlyCashFlowResult>();

        for (int month = 1; month <= 12; month++)
        {
            var income = incomes
                .FirstOrDefault(x => x.Month == month)?.Total ?? 0;

            var expense = expenses
                .FirstOrDefault(x => x.Month == month)?.Total ?? 0;

            result.Add(new MonthlyCashFlowResult
            {
                Year = year,
                Month = month,
                TotalIncome = income,
                TotalExpense = expense,
                NetCashFlow = income - expense
            });
        }

        return result;
    }

    public async Task<decimal> GetAnnualNetCashFlow(
        int propertyId,
        int year)
    {
        var income = await _context.PropertyIncomes
            .Where(x =>
                x.PropertyId == propertyId &&
                x.IsReceived &&
                x.IncomeDate.Year == year)
            .SumAsync(x => x.Amount);

        var expense = await _context.PropertyExpenses
            .Where(x =>
                x.PropertyId == propertyId &&
                x.IsPaid &&
                x.ExpenseDate.Year == year)
            .SumAsync(x => x.Amount);

        return income - expense;
    }
}

public class MonthlyCashFlowResult
{
    public int Year { get; set; }

    public int Month { get; set; }

    public decimal TotalIncome { get; set; }

    public decimal TotalExpense { get; set; }

    public decimal NetCashFlow { get; set; }
}
