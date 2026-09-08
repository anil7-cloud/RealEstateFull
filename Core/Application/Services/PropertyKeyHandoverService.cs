using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyKeyHandoverService
{
    private readonly AppDbContext _context;

    public PropertyKeyHandoverService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PropertyKeyHandover> CreateHandover(
        int propertyId,
        int? deliveredByUserId,
        int? receivedByUserId,
        string receiverName,
        int keyCount,
        string notes)
    {
        if (propertyId <= 0)
            throw new ArgumentOutOfRangeException(nameof(propertyId));

        if (keyCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(keyCount));

        if (string.IsNullOrWhiteSpace(receiverName))
        {
            throw new ArgumentException(
                "Teslim alan kişi adı boş olamaz.",
                nameof(receiverName));
        }

        var handover = new PropertyKeyHandover
        {
            PropertyId = propertyId,
            DeliveredByUserId = deliveredByUserId,
            ReceivedByUserId = receivedByUserId,
            ReceiverName = receiverName.Trim(),
            KeyCount = keyCount,
            HandoverDate = DateTime.UtcNow,
            Status = "Delivered",
            Notes = notes.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        _context.PropertyKeyHandovers.Add(handover);
        await _context.SaveChangesAsync();

        return handover;
    }

    public async Task<PropertyKeyHandover?> GetHandoverById(int id)
    {
        return await _context.PropertyKeyHandovers
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<PropertyKeyHandover>> GetPropertyHandovers(
        int propertyId)
    {
        return await _context.PropertyKeyHandovers
            .Where(x => x.PropertyId == propertyId)
            .OrderByDescending(x => x.HandoverDate)
            .ToListAsync();
    }

    public async Task<List<PropertyKeyHandover>> GetUnreturnedKeys()
    {
        return await _context.PropertyKeyHandovers
            .Where(x =>
                x.Status == "Delivered" &&
                x.ReturnDate == null)
            .OrderBy(x => x.HandoverDate)
            .ToListAsync();
    }

    public async Task<bool> ReturnKeys(
        int handoverId,
        DateTime returnDate)
    {
        var handover = await _context.PropertyKeyHandovers
            .FirstOrDefaultAsync(x => x.Id == handoverId);

        if (handover is null)
            return false;

        if (returnDate < handover.HandoverDate)
        {
            throw new ArgumentException(
                "İade tarihi teslim tarihinden önce olamaz.",
                nameof(returnDate));
        }

        handover.ReturnDate = returnDate;
        handover.Status = "Returned";

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteHandover(int id)
    {
        var handover = await _context.PropertyKeyHandovers
            .FirstOrDefaultAsync(x => x.Id == id);

        if (handover is null)
            return false;

        _context.PropertyKeyHandovers.Remove(handover);
        await _context.SaveChangesAsync();

        return true;
    }
}
