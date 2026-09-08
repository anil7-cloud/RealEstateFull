using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyTaxService
{
    private readonly AppDbContext _context;

    public PropertyTaxService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PropertyTax> CreateTax(PropertyTax tax)
    {
        _context.PropertyTaxes.Add(tax);

        await _context.SaveChangesAsync();

        return tax;
    }

    public async Task<List<PropertyTax>> GetPropertyTaxes(
        int propertyId)
    {
        return await _context.PropertyTaxes
            .Where(x => x.PropertyId == propertyId)
            .OrderByDescending(x => x.TaxYear)
            .ToListAsync();
    }

    public async Task<List<PropertyTax>> GetUnpaidTaxes()
    {
        return await _context.PropertyTaxes
            .Where(x => !x.IsPaid)
            .OrderBy(x => x.DueDate)
            .ToListAsync();
    }

    public async Task<bool> MarkAsPaid(
        int id,
        string receiptNumber)
    {
        var tax = await _context.PropertyTaxes
            .FirstOrDefaultAsync(x => x.Id == id);

        if (tax is null)
            return false;

        tax.IsPaid = true;
        tax.PaymentDate = DateTime.UtcNow;
        tax.ReceiptNumber = receiptNumber.Trim();

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteTax(int id)
    {
        var tax = await _context.PropertyTaxes
            .FirstOrDefaultAsync(x => x.Id == id);

        if (tax is null)
            return false;

        _context.PropertyTaxes.Remove(tax);

        await _context.SaveChangesAsync();

        return true;
    }
}
