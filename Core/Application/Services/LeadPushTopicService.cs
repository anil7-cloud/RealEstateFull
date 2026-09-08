using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadPushTopicService
{
    private readonly AppDbContext _context;

    public LeadPushTopicService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<LeadPushTopic> CreateAsync(
        string name,
        string description)
    {
        var topic = new LeadPushTopic
        {
            Name = name,
            Description = description,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.LeadPushTopics.Add(topic);

        await _context.SaveChangesAsync();

        return topic;
    }


    public async Task<List<LeadPushTopic>> GetActiveAsync()
    {
        return await _context.LeadPushTopics
            .Where(x => x.IsActive)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }


    public async Task<bool> DisableAsync(int id)
    {
        var topic = await _context.LeadPushTopics
            .FirstOrDefaultAsync(x => x.Id == id);

        if (topic == null)
            return false;

        topic.IsActive = false;
        topic.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
