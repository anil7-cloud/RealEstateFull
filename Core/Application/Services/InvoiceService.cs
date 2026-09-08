using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class InvoiceService
{
    private readonly AppDbContext _context;

    public InvoiceService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Invoice> CreateInvoice(
        int userId,
        decimal amount,
        string description)
    {
        var invoice = new Invoice
        {
            UserId = userId,
            Amount = amount,
            Description = description,
            Status = "Pending",
            CreatedAt = DateTime.UtcNow
        };

        _context.Invoices.Add(invoice);

        await _context.SaveChangesAsync();

        return invoice;
    }
}
