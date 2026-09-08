using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyReservationService
{
    private readonly AppDbContext _context;

    public PropertyReservationService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PropertyReservation> CreateReservation(
        int propertyId,
        int userId,
        decimal reservationFee,
        DateTime expirationDate,
        string notes)
    {
        if (reservationFee < 0)
            throw new ArgumentOutOfRangeException(
                nameof(reservationFee));

        var reservation = new PropertyReservation
        {
            PropertyId = propertyId,
            UserId = userId,
            ReservationFee = reservationFee,
            ReservationDate = DateTime.UtcNow,
            ExpirationDate = expirationDate,
            Notes = notes.Trim(),
            Status = "Active",
            CreatedAt = DateTime.UtcNow
        };

        _context.PropertyReservations.Add(reservation);

        await _context.SaveChangesAsync();

        return reservation;
    }

    public async Task<List<PropertyReservation>>
        GetPropertyReservations(int propertyId)
    {
        return await _context.PropertyReservations
            .Where(x => x.PropertyId == propertyId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<List<PropertyReservation>>
        GetUserReservations(int userId)
    {
        return await _context.PropertyReservations
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<bool> CancelReservation(int id)
    {
        var reservation = await _context.PropertyReservations
            .FirstOrDefaultAsync(x => x.Id == id);

        if (reservation is null)
            return false;

        reservation.Status = "Cancelled";

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> CompleteReservation(int id)
    {
        var reservation = await _context.PropertyReservations
            .FirstOrDefaultAsync(x => x.Id == id);

        if (reservation is null)
            return false;

        reservation.Status = "Completed";

        await _context.SaveChangesAsync();

        return true;
    }
}
