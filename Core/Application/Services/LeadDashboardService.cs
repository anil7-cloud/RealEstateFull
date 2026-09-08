using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadDashboardService
{
    private readonly AppDbContext _context;

    public LeadDashboardService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<LeadDashboard> GetSummaryAsync()
    {
        var dashboard = new LeadDashboard();

        dashboard.TotalLeads = await _context.Leads.CountAsync();

        dashboard.NewLeads = await _context.Leads
            .Where(x => x.CreatedAt >= DateTime.UtcNow.AddDays(-30))
            .CountAsync();

        dashboard.ConvertedLeads = await _context.LeadPipelines
            .CountAsync(x => x.IsWon);

        dashboard.LostLeads = await _context.LeadPipelines
            .CountAsync(x => x.IsLost);

        dashboard.QualifiedLeads = await _context.LeadPipelines
            .CountAsync(x => x.Probability >= 70);


        dashboard.ConversionRate =
            dashboard.TotalLeads == 0
            ? 0
            : (decimal)dashboard.ConvertedLeads /
              dashboard.TotalLeads * 100;


        dashboard.CreatedAt = DateTime.UtcNow;

        return dashboard;
    }


    public async Task<object> GetStatisticsAsync()
    {
        return new
        {
            TotalLeads = await _context.Leads.CountAsync(),

            ActivePipeline =
                await _context.LeadPipelines
                .CountAsync(x => !x.IsWon && !x.IsLost),

            Meetings =
                await _context.LeadMeetings.CountAsync(),

            Calls =
                await _context.LeadCalls.CountAsync(),

            Tasks =
                await _context.LeadTasks.CountAsync()
        };
    }


    public async Task<object> GetPipelineAsync()
    {
        return await _context.LeadPipelines
            .GroupBy(x => x.Stage)
            .Select(x => new
            {
                Stage = x.Key,
                Count = x.Count()
            })
            .ToListAsync();
    }


    public async Task<object> GetPerformanceAsync()
    {
        return new
        {
            CompletedTasks =
                await _context.LeadTasks
                .CountAsync(x => x.IsCompleted),

            CompletedMeetings =
                await _context.LeadMeetings
                .CountAsync(x => x.Status == "Completed"),

            CompletedCalls =
                await _context.LeadCalls
                .CountAsync(x => x.Status == "Completed")
        };
    }
}
