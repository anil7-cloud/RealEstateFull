using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadAssignmentService
{
    private readonly AppDbContext _context;

    public LeadAssignmentService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<LeadAssignment> CreateAsync(
        int leadId,
        int assignedUserId,
        string reason)
    {
        var assignment = new LeadAssignment
        {
            LeadId = leadId,
            AssignedUserId = assignedUserId,
            AssignmentType = "Manual",
            Status = "Active",
            Reason = reason,
            IsPrimary = true,
            IsActive = true,
            AssignedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };

        _context.LeadAssignments.Add(assignment);

        await _context.SaveChangesAsync();

        return assignment;
    }


    public async Task<List<LeadAssignment>> GetByLeadAsync(
        int leadId)
    {
        return await _context.LeadAssignments
            .Where(x => x.LeadId == leadId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<bool> CancelAsync(
        int id)
    {
        var assignment = await _context.LeadAssignments
            .FirstOrDefaultAsync(x => x.Id == id);

        if (assignment == null)
            return false;

        assignment.Status = "Cancelled";
        assignment.IsActive = false;
        assignment.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
