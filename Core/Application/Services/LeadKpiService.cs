using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadKpiService
{
    private readonly AppDbContext _context;

    public LeadKpiService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<object> GetSummaryAsync()
    {
        var totalLeads = await _context.Leads.CountAsync();

        var converted =
            await _context.LeadPipelines
            .CountAsync(x => x.IsWon);

        var lost =
            await _context.LeadPipelines
            .CountAsync(x => x.IsLost);


        return new
        {
            TotalLeads = totalLeads,
            ConvertedLeads = converted,
            LostLeads = lost,

            ConversionRate =
                totalLeads == 0
                ? 0
                : (decimal)converted / totalLeads * 100
        };
    }


    public async Task<object> GetActivityAsync()
    {
        return new
        {
            Calls =
                await _context.LeadCalls.CountAsync(),

            Meetings =
                await _context.LeadMeetings.CountAsync(),

            Tasks =
                await _context.LeadTasks.CountAsync()
        };
    }


    public async Task<object> GetRevenueAsync()
    {
        return new
        {
            TotalValue =
                await _context.LeadConversions
                .SumAsync(x => x.FinalValue),

            WonDeals =
                await _context.LeadConversions
                .CountAsync(x => x.IsWon)
        };
    }
}
