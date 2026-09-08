using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyDocumentService
{
    private readonly AppDbContext _context;

    public PropertyDocumentService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<List<PropertyDocument>> GetDocuments(Guid propertyId)
    {
        return await _context.PropertyDocuments
            .Where(x => x.PropertyListingId == propertyId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<bool> DeleteDocument(Guid id)
    {
        var document = await _context.PropertyDocuments
            .FirstOrDefaultAsync(x => x.Id == id);

        if (document == null)
            return false;

        _context.PropertyDocuments.Remove(document);

        await _context.SaveChangesAsync();

        return true;
    }
}
