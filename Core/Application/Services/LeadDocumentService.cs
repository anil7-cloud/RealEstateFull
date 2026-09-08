using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadDocumentService
{
    private readonly AppDbContext _context;

    public LeadDocumentService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<List<LeadDocument>> GetAllAsync()
    {
        return await _context.LeadDocuments
            .Where(x => x.IsActive)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<List<LeadDocument>> GetByLeadAsync(int leadId)
    {
        return await _context.LeadDocuments
            .Where(x => x.LeadId == leadId && x.IsActive)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<LeadDocument> CreateAsync(LeadDocument document)
    {
        document.CreatedAt = DateTime.UtcNow;
        document.UploadedAt = DateTime.UtcNow;
        document.IsActive = true;

        _context.LeadDocuments.Add(document);

        await _context.SaveChangesAsync();

        return document;
    }


    public async Task<bool> DeleteAsync(int id)
    {
        var document = await _context.LeadDocuments
            .FirstOrDefaultAsync(x => x.Id == id);

        if (document == null)
            return false;

        document.IsActive = false;
        document.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
