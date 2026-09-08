using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Domain.Entities;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationRetryAlertRepository
    {
        private readonly AppDbContext _dbContext;

        private readonly
            PropertyCustomerMatchSalesAutomationRetryAlertHistoryRepository
                _historyRepository;

        public PropertyCustomerMatchSalesAutomationRetryAlertRepository(
            AppDbContext dbContext,
            PropertyCustomerMatchSalesAutomationRetryAlertHistoryRepository historyRepository)
        {
            _dbContext = dbContext;

            _historyRepository =
                historyRepository;
        }

        public async Task<PropertyMatchSalesAutomationRetryAlert>
            CreateAsync(
                PropertyMatchSalesAutomationRetryAlert alert,
                CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(alert);

            var existing =
                await _dbContext
                    .PropertyMatchSalesAutomationRetryAlerts
                    .FirstOrDefaultAsync(
                        x =>
                            x.Code == alert.Code &&
                            x.Status != "Resolved",
                        cancellationToken);

            if (existing != null)
            {
                existing.Severity =
                    alert.Severity;

                existing.Category =
                    alert.Category;

                existing.Message =
                    alert.Message;

                existing.UpdatedAt =
                    DateTime.UtcNow;

                await _dbContext
                    .SaveChangesAsync(
                        cancellationToken);

                return existing;
            }

            if (alert.Id == Guid.Empty)
            {
                alert.Id =
                    Guid.NewGuid();
            }

            alert.Status =
                "Active";

            alert.CreatedAt =
                DateTime.UtcNow;

            alert.UpdatedAt =
                DateTime.UtcNow;

            await _dbContext
                .PropertyMatchSalesAutomationRetryAlerts
                .AddAsync(
                    alert,
                    cancellationToken);

            await _dbContext
                .SaveChangesAsync(
                    cancellationToken);

            await _historyRepository
                .AddAsync(
                    alert.Id,
                    alert.Code,
                    "Created",
                    null,
                    alert.Status,
                    alert.Severity,
                    "System",
                    alert.Message,
                    cancellationToken);

            return alert;
        }

        public async Task<
            PropertyMatchSalesAutomationRetryAlert?>
            GetByIdAsync(
                Guid id,
                CancellationToken cancellationToken = default)
        {
            return await _dbContext
                .PropertyMatchSalesAutomationRetryAlerts
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task<List<
            PropertyMatchSalesAutomationRetryAlert>>
            GetActiveAsync(
                int limit = 100,
                CancellationToken cancellationToken = default)
        {
            limit =
                Math.Clamp(
                    limit,
                    1,
                    1000);

            return await _dbContext
                .PropertyMatchSalesAutomationRetryAlerts
                .AsNoTracking()
                .Where(x =>
                    x.Status == "Active" ||
                    x.Status == "Acknowledged")
                .OrderByDescending(x =>
                    x.CreatedAt)
                .Take(limit)
                .ToListAsync(
                    cancellationToken);
        }

        public async Task<List<
            PropertyMatchSalesAutomationRetryAlert>>
            GetRecentAsync(
                int limit = 100,
                CancellationToken cancellationToken = default)
        {
            limit =
                Math.Clamp(
                    limit,
                    1,
                    1000);

            return await _dbContext
                .PropertyMatchSalesAutomationRetryAlerts
                .AsNoTracking()
                .OrderByDescending(x =>
                    x.CreatedAt)
                .Take(limit)
                .ToListAsync(
                    cancellationToken);
        }

        public async Task<
            PropertyMatchSalesAutomationRetryAlert?>
            AcknowledgeAsync(
                Guid id,
                string? acknowledgedBy,
                CancellationToken cancellationToken = default)
        {
            var alert =
                await _dbContext
                    .PropertyMatchSalesAutomationRetryAlerts
                    .FirstOrDefaultAsync(
                        x => x.Id == id,
                        cancellationToken);

            if (alert == null)
            {
                return null;
            }

            if (alert.Status == "Resolved")
            {
                return alert;
            }

            var previousStatus =
                alert.Status;

            alert.Status =
                "Acknowledged";

            alert.AcknowledgedAt =
                DateTime.UtcNow;

            alert.AcknowledgedBy =
                string.IsNullOrWhiteSpace(acknowledgedBy)
                    ? "System"
                    : acknowledgedBy.Trim();

            alert.UpdatedAt =
                DateTime.UtcNow;

            await _dbContext
                .SaveChangesAsync(
                    cancellationToken);

            await _historyRepository
                .AddAsync(
                    alert.Id,
                    alert.Code,
                    "Acknowledged",
                    previousStatus,
                    alert.Status,
                    alert.Severity,
                    alert.AcknowledgedBy,
                    alert.Message,
                    cancellationToken);

            return alert;
        }

        public async Task<
            PropertyMatchSalesAutomationRetryAlert?>
            ResolveAsync(
                Guid id,
                string? resolvedBy,
                CancellationToken cancellationToken = default)
        {
            var alert =
                await _dbContext
                    .PropertyMatchSalesAutomationRetryAlerts
                    .FirstOrDefaultAsync(
                        x => x.Id == id,
                        cancellationToken);

            if (alert == null)
            {
                return null;
            }

            if (alert.Status == "Resolved")
            {
                return alert;
            }

            var previousResolveStatus =
                alert.Status;

            alert.Status =
                "Resolved";

            alert.ResolvedAt =
                DateTime.UtcNow;

            alert.ResolvedBy =
                string.IsNullOrWhiteSpace(resolvedBy)
                    ? "System"
                    : resolvedBy.Trim();

            alert.UpdatedAt =
                DateTime.UtcNow;

            await _dbContext
                .SaveChangesAsync(
                    cancellationToken);

            return alert;
        }

        public async Task<int>
            ResolveByCodeAsync(
                string code,
                string resolvedBy = "System",
                CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return 0;
            }

            var alerts =
                await _dbContext
                    .PropertyMatchSalesAutomationRetryAlerts
                    .Where(x =>
                        x.Code == code &&
                        x.Status != "Resolved")
                    .ToListAsync(
                        cancellationToken);

            if (alerts.Count == 0)
            {
                return 0;
            }

            var now =
                DateTime.UtcNow;

            foreach (var alert in alerts)
            {
                alert.Status =
                    "Resolved";

                alert.ResolvedAt =
                    now;

                alert.ResolvedBy =
                    resolvedBy;

                alert.UpdatedAt =
                    now;
            }

            await _dbContext
                .SaveChangesAsync(
                    cancellationToken);

            return alerts.Count;
        }

        public async Task<List<
            PropertyMatchSalesAutomationRetryAlert>>
            GetCreatedSinceAsync(
                DateTime since,
                int limit = 5000,
                CancellationToken cancellationToken = default)
        {
            limit =
                Math.Clamp(
                    limit,
                    1,
                    10000);

            return await _dbContext
                .PropertyMatchSalesAutomationRetryAlerts
                .AsNoTracking()
                .Where(x =>
                    x.CreatedAt >= since)
                .OrderByDescending(x =>
                    x.CreatedAt)
                .Take(limit)
                .ToListAsync(
                    cancellationToken);
        }

        public async Task<bool>
            HasActiveAlertAsync(
                string code,
                CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return false;
            }

            return await _dbContext
                .PropertyMatchSalesAutomationRetryAlerts
                .AsNoTracking()
                .AnyAsync(
                    x =>
                        x.Code == code &&
                        x.Status != "Resolved",
                    cancellationToken);
        }
    }
}
