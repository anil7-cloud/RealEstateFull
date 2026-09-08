using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadFeedbackService
{
    private readonly AppDbContext _context;

    public LeadFeedbackService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<LeadFeedback> CreateAsync(
        LeadFeedback feedback)
    {
        feedback.CreatedAt = DateTime.UtcNow;
        feedback.IsActive = true;

        _context.LeadFeedbacks.Add(feedback);

        await _context.SaveChangesAsync();

        return feedback;
    }


    public async Task<List<LeadFeedback>> GetByLeadAsync(int leadId)
    {
        return await _context.LeadFeedbacks
            .Where(x => x.LeadId == leadId && x.IsActive)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<bool> UpdateAsync(
        LeadFeedback updated)
    {
        var feedback = await _context.LeadFeedbacks
            .FirstOrDefaultAsync(x => x.Id == updated.Id);

        if(feedback == null)
            return false;


        feedback.FeedbackType = updated.FeedbackType;
        feedback.Title = updated.Title;
        feedback.Comment = updated.Comment;
        feedback.Rating = updated.Rating;
        feedback.WouldRecommend = updated.WouldRecommend;
        feedback.RequiresAction = updated.RequiresAction;
        feedback.ActionTaken = updated.ActionTaken;
        feedback.Notes = updated.Notes;
        feedback.UpdatedAt = DateTime.UtcNow;


        await _context.SaveChangesAsync();

        return true;
    }
}
