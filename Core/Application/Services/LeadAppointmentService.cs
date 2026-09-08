using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadAppointmentService
{
    private readonly AppDbContext _context;

    public LeadAppointmentService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<LeadAppointment> CreateAsync(
        int leadId,
        int propertyId,
        string title,
        DateTime startTime,
        DateTime endTime)
    {
        var appointment = new LeadAppointment
        {
            LeadId = leadId,
            PropertyId = propertyId,
            Title = title,
            Description = string.Empty,
            AppointmentType = "Office",
            StartTime = startTime,
            EndTime = endTime,
            Status = "Scheduled",
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.LeadAppointments.Add(appointment);

        await _context.SaveChangesAsync();

        return appointment;
    }


    public async Task<List<LeadAppointment>> GetByLeadAsync(
        int leadId)
    {
        return await _context.LeadAppointments
            .Where(x => x.LeadId == leadId)
            .OrderBy(x => x.StartTime)
            .ToListAsync();
    }


    public async Task<bool> CompleteAsync(
        int id)
    {
        var appointment = await _context.LeadAppointments
            .FirstOrDefaultAsync(x => x.Id == id);

        if (appointment == null)
            return false;

        appointment.Status = "Completed";
        appointment.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> DeleteAsync(
        int id)
    {
        var appointment = await _context.LeadAppointments
            .FirstOrDefaultAsync(x => x.Id == id);

        if (appointment == null)
            return false;

        appointment.IsActive = false;
        appointment.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
