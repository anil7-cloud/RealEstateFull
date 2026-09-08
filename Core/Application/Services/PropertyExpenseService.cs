
using Microsoft.EntityFrameworkCore;

using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyExpenseService

{

    private readonly AppDbContext _context;

    public PropertyExpenseService(AppDbContext context)

    {

        _context = context;

    }

    public async Task<PropertyExpense> CreateExpense(

        PropertyExpense expense)

    {

        _context.PropertyExpenses.Add(expense);

        await _context.SaveChangesAsync();

        return expense;

    }

    public async Task<List<PropertyExpense>> GetPropertyExpenses(

        int propertyId)

    {

        return await _context.PropertyExpenses

            .Where(x => x.PropertyId == propertyId)

            .OrderByDescending(x => x.ExpenseDate)

            .ToListAsync();

    }

    public async Task<decimal> GetTotalExpenses(

        int propertyId)

    {

        return await _context.PropertyExpenses

            .Where(x => x.PropertyId == propertyId)

            .SumAsync(x => x.Amount);

    }

    public async Task<bool> MarkAsPaid(int id)

    {

        var expense = await _context.PropertyExpenses

            .FirstOrDefaultAsync(x => x.Id == id);

        if (expense is null)

            return false;

        expense.IsPaid = true;

        await _context.SaveChangesAsync();

        return true;

    }

    public async Task<bool> DeleteExpense(int id)

    {

        var expense = await _context.PropertyExpenses

            .FirstOrDefaultAsync(x => x.Id == id);

        if (expense is null)

            return false;

        _context.PropertyExpenses.Remove(expense);

        await _context.SaveChangesAsync();

        return true;

    }

}

