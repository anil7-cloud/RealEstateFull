using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class AuditLogService
{
    private readonly AppDbContext _context;

    public AuditLogService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<AuditLog> CreateLog(
        int? userId,
        string action,
        string entityName,
        int? entityId,
        string details,
        string ipAddress)
    {
        var log = new AuditLog
        {
            UserId = userId,
            Action = action,
            EntityName = entityName,
            EntityId = entityId,
            Details = details,
            IpAddress = ipAddress,
            CreatedAt = DateTime.UtcNow
        };

        _context.AuditLogs.Add(log);

        await _context.SaveChangesAsync();

        return log;
    }


    public async Task<AuditLog?> GetLogById(int id)
    {
        return await _context.AuditLogs
            .FirstOrDefaultAsync(x => x.Id == id);
    }

}
