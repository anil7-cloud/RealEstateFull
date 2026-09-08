using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadInteractionService
{
    private readonly AppDbContext _context;

    public LeadInteractionService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<LeadInteraction> CreateAsync(
        LeadInteraction interaction)
    {
        interaction.CreatedAt = DateTime.UtcNow;

        _context.LeadInteractions.Add(interaction);

        await _context.SaveChangesAsync();

        return interaction;
    }


    public async Task<List<LeadInteraction>> GetByLeadAsync(
        int leadId)
    {
        return await _context.LeadInteractions
            .Where(x => x.LeadId == leadId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<LeadInteraction?> GetByIdAsync(
        int id)
    {
        return await _context.LeadInteractions
            .FirstOrDefaultAsync(x => x.Id == id);
    }


    public async Task<bool> UpdateAsync(
        LeadInteraction updated)
    {
        var interaction = await _context.LeadInteractions
            .FirstOrDefaultAsync(x => x.Id == updated.Id);

        if (interaction == null)
            return false;


        interaction.InteractionType = updated.InteractionType;
        interaction.Description = updated.Description;
        interaction.UpdatedAt = DateTime.UtcNow;


        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> DeleteAsync(
        int id)
    {
        var interaction = await _context.LeadInteractions
            .FirstOrDefaultAsync(x => x.Id == id);

        if (interaction == null)
            return false;


        _context.LeadInteractions.Remove(interaction);

        await _context.SaveChangesAsync();

        return true;
    }
}
