using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadPushStatisticService
{
    private readonly AppDbContext _context;

    public LeadPushStatisticService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<LeadPushStatistic> CreateAsync(
        int campaignId,
        int sent,
        int delivered,
        int opened)
    {
        var statistic = new LeadPushStatistic
        {
            LeadPushCampaignId = campaignId,
            TotalSent = sent,
            TotalDelivered = delivered,
            TotalOpened = opened,
            TotalFailed = sent - delivered,
            CreatedAt = DateTime.UtcNow
        };

        _context.LeadPushStatistics.Add(statistic);

        await _context.SaveChangesAsync();

        return statistic;
    }


    public async Task<List<LeadPushStatistic>> GetByCampaignAsync(
        int campaignId)
    {
        return await _context.LeadPushStatistics
            .Where(x => x.LeadPushCampaignId == campaignId)
            .OrderByDescending(x => x.StatisticDate)
            .ToListAsync();
    }
}
