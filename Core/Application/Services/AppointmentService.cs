using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class AppointmentService
{
    private readonly AppDbContext _context;

    public AppointmentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Appointment?> CreateAppointment(
        int propertyId,
        int userId,
        DateTime appointmentDate,
        string note)
    {
        if (userId <= 0 || propertyId <= 0)
            return null;

        var appointment = new Appointment
        {
            PropertyId = propertyId,
            UserId = userId,
            AppointmentDate = appointmentDate,
            Note = note?.Trim() ?? string.Empty,
            Status = "Pending",
            CreatedAt = DateTime.UtcNow
        };

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();

        return appointment;
    }

    public async Task<Appointment?> GetAppointmentById(
        int id,
        int userId)
    {
        if (id <= 0 || userId <= 0)
            return null;

        return await _context.Appointments
            .FirstOrDefaultAsync(x =>
                x.Id == id &&
                x.UserId == userId);
    }

    public async Task<List<Appointment>> GetAllAppointments()
    {
        return await _context.Appointments
            .OrderByDescending(x => x.AppointmentDate)
            .ToListAsync();
    }

    public async Task<List<Appointment>> GetUserAppointments(
        int userId)
    {
        if (userId <= 0)
            return new List<Appointment>();

        return await _context.Appointments
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.AppointmentDate)
            .ToListAsync();
    }

    public async Task<List<Appointment>> GetPropertyAppointments(
        int propertyId)
    {
        if (propertyId <= 0)
            return new List<Appointment>();

        return await _context.Appointments
            .Where(x => x.PropertyId == propertyId)
            .OrderByDescending(x => x.AppointmentDate)
            .ToListAsync();
    }

    public async Task<bool> UpdateAppointmentStatus(
        int appointmentId,
        int userId,
        string status)
    {
        if (appointmentId <= 0 || userId <= 0)
            return false;

        if (string.IsNullOrWhiteSpace(status))
            return false;

        var appointment = await _context.Appointments
            .FirstOrDefaultAsync(x =>
                x.Id == appointmentId &&
                x.UserId == userId);

        if (appointment is null)
            return false;

        appointment.Status = status.Trim();

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAppointment(
        int appointmentId,
        int userId)
    {
        if (appointmentId <= 0 || userId <= 0)
            return false;

        var appointment = await _context.Appointments
            .FirstOrDefaultAsync(x =>
                x.Id == appointmentId &&
                x.UserId == userId);

        if (appointment is null)
            return false;

        _context.Appointments.Remove(appointment);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<Appointment>> GetPendingAppointments()
    {
        return await _context.Appointments
            .Where(x => x.Status == "Pending")
            .OrderBy(x => x.AppointmentDate)
            .ToListAsync();
    }

    public async Task<List<Appointment>> GetApprovedAppointments()
    {
        return await _context.Appointments
            .Where(x => x.Status == "Approved")
            .OrderBy(x => x.AppointmentDate)
            .ToListAsync();
    }

    public async Task<List<Appointment>> GetRejectedAppointments()
    {
        return await _context.Appointments
            .Where(x => x.Status == "Rejected")
            .OrderBy(x => x.AppointmentDate)
            .ToListAsync();
    }

    public async Task<List<Appointment>> GetAppointmentsBetweenDates(
        DateTime startDate,
        DateTime endDate)
    {
        return await _context.Appointments
            .Where(x =>
                x.AppointmentDate >= startDate &&
                x.AppointmentDate <= endDate)
            .OrderBy(x => x.AppointmentDate)
            .ToListAsync();
    }
}
