using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadNoteService
{
    private readonly AppDbContext _context;

    public LeadNoteService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<List<LeadNote>> GetAllAsync()
    {
        return await _context.LeadNotes
            .Where(x => x.IsActive)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<List<LeadNote>> GetByLeadAsync(int leadId)
    {
        return await _context.LeadNotes
            .Where(x => x.LeadId == leadId && x.IsActive)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<LeadNote> CreateAsync(LeadNote note)
    {
        note.CreatedAt = DateTime.UtcNow;
        note.IsActive = true;

        _context.LeadNotes.Add(note);

        await _context.SaveChangesAsync();

        return note;
    }


    public async Task<bool> DeleteAsync(int id)
    {
        var note = await _context.LeadNotes
            .FirstOrDefaultAsync(x => x.Id == id);

        if (note == null)
            return false;

        note.IsActive = false;
        note.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
