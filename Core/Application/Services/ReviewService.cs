using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class ReviewService
{
    private readonly AppDbContext _context;

    public ReviewService(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddReview(int propertyId, int userId, int rating, string comment)
    {
        var review = new Review
        {
            PropertyId = propertyId,
            UserId = userId,
            Rating = rating,
            Comment = comment,
            CreatedAt = DateTime.UtcNow
        };

        _context.Reviews.Add(review);

        await _context.SaveChangesAsync();
    }

    public async Task<List<Review>> GetPropertyReviews(int propertyId)
    {
        return await _context.Reviews
            .Where(x => x.PropertyId == propertyId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<double> GetAverageRating(int propertyId)
    {
        var reviews = await _context.Reviews
            .Where(x => x.PropertyId == propertyId)
            .ToListAsync();

        if (reviews.Count == 0)
            return 0;

        return reviews.Average(x => x.Rating);
    }
}
