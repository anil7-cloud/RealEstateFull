using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class WorkflowAnalyticsService
{
    private readonly AppDbContext _context;

    public WorkflowAnalyticsService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<int> GetTotalExecutions()
    {
        return await _context.LeadWorkflowExecutions
            .CountAsync();
    }


    public async Task<int> GetCompletedExecutions()
    {
        return await _context.LeadWorkflowExecutions
            .CountAsync(x => x.Status == "Completed");
    }


    public async Task<int> GetFailedExecutions()
    {
        return await _context.LeadWorkflowExecutions
            .CountAsync(x => x.Status == "Failed");
    }


    public async Task<double> GetSuccessRate()
    {
        var total = await GetTotalExecutions();

        if (total == 0)
            return 0;

        var completed = await GetCompletedExecutions();

        return (double)completed / total * 100;
    }
}
