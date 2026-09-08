using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyViewingFeedbackService
{
    private readonly AppDbContext _context;

    public PropertyViewingFeedbackService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PropertyViewingFeedback> CreateFeedback(
        PropertyViewingFeedback feedback)
    {
        feedback.Rating = Math.Clamp(feedback.Rating, 1, 5);
        feedback.CreatedAt = DateTime.UtcNow;

        _context.PropertyViewingFeedbacks.Add(feedback);

        await _context.SaveChangesAsync();

        return feedback;
    }

    public async Task<List<PropertyViewingFeedback>> GetPropertyFeedbacks(
        int propertyId)
    {
        return await _context.PropertyViewingFeedbacks
            .Where(x => x.PropertyId == propertyId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<double> GetAverageRating(
        int propertyId)
    {
        var ratings = await _context.PropertyViewingFeedbacks
            .Where(x => x.PropertyId == propertyId)
            .Select(x => x.Rating)
            .ToListAsync();

        if (ratings.Count == 0)
            return 0;

        return Math.Round(ratings.Average(), 2);
    }

    public async Task<bool> DeleteFeedback(int id)
    {
        var feedback = await _context.PropertyViewingFeedbacks
            .FirstOrDefaultAsync(x => x.Id == id);

        if (feedback is null)
            return false;

        _context.PropertyViewingFeedbacks.Remove(feedback);

        await _context.SaveChangesAsync();

        return true;
    }
}
