using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyUtilityBillService
{
    private readonly AppDbContext _context;

    public PropertyUtilityBillService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PropertyUtilityBill> CreateBill(
        PropertyUtilityBill bill)
    {
        _context.PropertyUtilityBills.Add(bill);

        await _context.SaveChangesAsync();

        return bill;
    }

    public async Task<List<PropertyUtilityBill>> GetBills(
        int propertyId)
    {
        return await _context.PropertyUtilityBills
            .Where(x => x.PropertyId == propertyId)
            .OrderByDescending(x => x.BillingPeriod)
            .ToListAsync();
    }

    public async Task<List<PropertyUtilityBill>> GetUnpaidBills()
    {
        return await _context.PropertyUtilityBills
            .Where(x => !x.IsPaid)
            .OrderBy(x => x.DueDate)
            .ToListAsync();
    }

    public async Task<bool> MarkAsPaid(
        int id,
        DateTime paymentDate)
    {
        var bill = await _context.PropertyUtilityBills
            .FirstOrDefaultAsync(x => x.Id == id);

        if (bill is null)
            return false;

        bill.IsPaid = true;
        bill.PaymentDate = paymentDate;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteBill(int id)
    {
        var bill = await _context.PropertyUtilityBills
            .FirstOrDefaultAsync(x => x.Id == id);

        if (bill is null)
            return false;

        _context.PropertyUtilityBills.Remove(bill);

        await _context.SaveChangesAsync();

        return true;
    }
}
