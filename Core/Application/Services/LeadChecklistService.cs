using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadChecklistService
{
    private readonly AppDbContext _context;

    public LeadChecklistService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<LeadChecklist> CreateAsync(
        int leadId,
        string title,
        string description)
    {
        var item = new LeadChecklist
        {
            LeadId = leadId,
            Title = title,
            Description = description,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.LeadChecklists.Add(item);

        await _context.SaveChangesAsync();

        return item;
    }


    public async Task<List<LeadChecklist>> GetByLeadAsync(
        int leadId)
    {
        return await _context.LeadChecklists
            .Where(x => x.LeadId == leadId)
            .OrderBy(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<bool> CompleteAsync(
        int id)
    {
        var item = await _context.LeadChecklists
            .FirstOrDefaultAsync(x => x.Id == id);

        if (item == null)
            return false;

        item.IsCompleted = true;

        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> DeleteAsync(
        int id)
    {
        var item = await _context.LeadChecklists
            .FirstOrDefaultAsync(x => x.Id == id);

        if (item == null)
            return false;

        _context.LeadChecklists.Remove(item);

        await _context.SaveChangesAsync();

        return true;
    }
}
