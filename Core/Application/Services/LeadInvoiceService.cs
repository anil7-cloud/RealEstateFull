using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadInvoiceService
{
    private readonly AppDbContext _context;

    public LeadInvoiceService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<LeadInvoice> CreateAsync(
        LeadInvoice invoice)
    {
        invoice.CreatedAt = DateTime.UtcNow;
        invoice.Status = "Pending";

        _context.LeadInvoices.Add(invoice);

        await _context.SaveChangesAsync();

        return invoice;
    }


    public async Task<List<LeadInvoice>> GetByLeadAsync(
        int leadId)
    {
        return await _context.LeadInvoices
            .Where(x => x.LeadId == leadId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<LeadInvoice?> GetByIdAsync(
        int id)
    {
        return await _context.LeadInvoices
            .FirstOrDefaultAsync(x => x.Id == id);
    }


    public async Task<bool> PayAsync(
        int id,
        decimal amount)
    {
        var invoice = await _context.LeadInvoices
            .FirstOrDefaultAsync(x => x.Id == id);

        if (invoice == null)
            return false;


        invoice.TotalAmount += amount;

        invoice.Status =
            invoice.TotalAmount >= invoice.TotalAmount
            ? "Paid"
            : "Partial";


        invoice.UpdatedAt = DateTime.UtcNow;


        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> UpdateAsync(
        LeadInvoice updated)
    {
        var invoice = await _context.LeadInvoices
            .FirstOrDefaultAsync(x => x.Id == updated.Id);

        if (invoice == null)
            return false;


        
        invoice.TotalAmount = updated.TotalAmount;
        invoice.DueDate = updated.DueDate;
        invoice.Status = updated.Status;
        invoice.UpdatedAt = DateTime.UtcNow;


        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> DeleteAsync(
        int id)
    {
        var invoice = await _context.LeadInvoices
            .FirstOrDefaultAsync(x => x.Id == id);

        if (invoice == null)
            return false;


        _context.LeadInvoices.Remove(invoice);

        await _context.SaveChangesAsync();

        return true;
    }
}
