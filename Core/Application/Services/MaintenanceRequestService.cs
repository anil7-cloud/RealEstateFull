using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class MaintenanceRequestService
{
    private readonly AppDbContext _context;

    public MaintenanceRequestService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<MaintenanceRequest> CreateRequest(
        int propertyId,
        int userId,
        string title,
        string description)
    {
        var request = new MaintenanceRequest
        {
            PropertyId = propertyId,
            UserId = userId,
            Title = title,
            Description = description,
            Status = "Open",
            CreatedAt = DateTime.UtcNow
        };

        _context.MaintenanceRequests.Add(request);

        await _context.SaveChangesAsync();

        return request;
    }
}
