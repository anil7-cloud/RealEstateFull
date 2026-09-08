using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadPushDeviceService
{
    private readonly AppDbContext _context;

    public LeadPushDeviceService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LeadPushDevice> CreateAsync(LeadPushDevice device)
    {
        device.RegisteredAt = DateTime.UtcNow;

        _context.LeadPushDevices.Add(device);

        await _context.SaveChangesAsync();

        return device;
    }

    public async Task<List<LeadPushDevice>> GetAllAsync()
    {
        return await _context.LeadPushDevices
            .OrderByDescending(x => x.RegisteredAt)
            .ToListAsync();
    }

    public async Task<LeadPushDevice?> GetByIdAsync(int id)
    {
        return await _context.LeadPushDevices
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<LeadPushDevice>> GetActiveDevicesAsync()
    {
        return await _context.LeadPushDevices
            .Where(x => x.IsActive)
            .OrderBy(x => x.DeviceName)
            .ToListAsync();
    }

    public async Task<List<LeadPushDevice>> GetByUserIdAsync(int userId)
    {
        return await _context.LeadPushDevices
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.RegisteredAt)
            .ToListAsync();
    }

    public async Task<bool> UpdateAsync(LeadPushDevice updated)
    {
        var device = await _context.LeadPushDevices
            .FirstOrDefaultAsync(x => x.Id == updated.Id);

        if (device == null)
            return false;

        device.DeviceToken = updated.DeviceToken;
        device.DeviceId = updated.DeviceId;
        device.Platform = updated.Platform;
        device.DeviceName = updated.DeviceName;
        device.Manufacturer = updated.Manufacturer;
        device.Model = updated.Model;
        device.OperatingSystem = updated.OperatingSystem;
        device.AppVersion = updated.AppVersion;
        device.Language = updated.Language;
        device.TimeZone = updated.TimeZone;
        device.IsActive = updated.IsActive;
        device.NotificationsEnabled = updated.NotificationsEnabled;
        device.LastSeenAt = updated.LastSeenAt;
        device.LastNotificationAt = updated.LastNotificationAt;
        device.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var device = await _context.LeadPushDevices
            .FirstOrDefaultAsync(x => x.Id == id);

        if (device == null)
            return false;

        _context.LeadPushDevices.Remove(device);

        await _context.SaveChangesAsync();

        return true;
    }
}
