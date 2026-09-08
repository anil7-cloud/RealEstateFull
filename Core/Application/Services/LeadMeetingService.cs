using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadMeetingService
{
    private readonly AppDbContext _context;

    public LeadMeetingService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<List<LeadMeeting>> GetAllAsync()
    {
        return await _context.LeadMeetings
            .OrderByDescending(x => x.StartTime)
            .ToListAsync();
    }


    public async Task<LeadMeeting?> GetByIdAsync(int id)
    {
        return await _context.LeadMeetings
            .FirstOrDefaultAsync(x => x.Id == id);
    }


    public async Task<List<LeadMeeting>> GetByLeadAsync(int leadId)
    {
        return await _context.LeadMeetings
            .Where(x => x.LeadId == leadId)
            .OrderBy(x => x.StartTime)
            .ToListAsync();
    }


    public async Task<LeadMeeting> CreateAsync(
        LeadMeeting meeting)
    {
        meeting.CreatedAt = DateTime.UtcNow;
        meeting.Status = "Scheduled";

        _context.LeadMeetings.Add(meeting);

        await _context.SaveChangesAsync();

        return meeting;
    }


    public async Task<bool> UpdateStatusAsync(
        int id,
        string status)
    {
        var meeting = await _context.LeadMeetings
            .FirstOrDefaultAsync(x => x.Id == id);

        if (meeting == null)
            return false;


        meeting.Status = status;
        meeting.UpdatedAt = DateTime.UtcNow;


        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> CompleteAsync(int id)
    {
        var meeting = await _context.LeadMeetings
            .FirstOrDefaultAsync(x => x.Id == id);

        if (meeting == null)
            return false;


        meeting.Status = "Completed";
        meeting.UpdatedAt = DateTime.UtcNow;


        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> CancelAsync(int id)
    {
        var meeting = await _context.LeadMeetings
            .FirstOrDefaultAsync(x => x.Id == id);

        if (meeting == null)
            return false;


        meeting.Status = "Cancelled";
        meeting.UpdatedAt = DateTime.UtcNow;


        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> DeleteAsync(int id)
    {
        var meeting = await _context.LeadMeetings
            .FirstOrDefaultAsync(x => x.Id == id);

        if (meeting == null)
            return false;


        _context.LeadMeetings.Remove(meeting);

        await _context.SaveChangesAsync();

        return true;
    }
}
