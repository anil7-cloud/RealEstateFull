using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class WorkflowReportService
{
    private readonly AppDbContext _context;

    public WorkflowReportService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<object> GetWorkflowReportAsync(
        int workflowId)
    {
        var totalSteps = await _context.LeadWorkflowSteps
            .CountAsync(x => x.LeadWorkflowId == workflowId);


        var completedSteps = await _context.LeadWorkflowSteps
            .CountAsync(x =>
                x.LeadWorkflowId == workflowId &&
                x.Status == "Completed");


        var pendingSteps = await _context.LeadWorkflowSteps
            .CountAsync(x =>
                x.LeadWorkflowId == workflowId &&
                x.Status == "Pending");


        return new
        {
            WorkflowId = workflowId,
            TotalSteps = totalSteps,
            CompletedSteps = completedSteps,
            PendingSteps = pendingSteps,
            CompletionRate =
                totalSteps == 0
                    ? 0
                    : (double)completedSteps / totalSteps * 100
        };
    }
}
