using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Domain.Entities;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationRetryAlertHistoryRepository
    {
        private readonly AppDbContext _dbContext;

        public PropertyCustomerMatchSalesAutomationRetryAlertHistoryRepository(
            AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PropertyMatchSalesAutomationRetryAlertHistory>
            AddAsync(
                Guid alertId,
                string alertCode,
                string action,
                string? previousStatus,
                string newStatus,
                string severity,
                string? performedBy = null,
                string? message = null,
                CancellationToken cancellationToken = default)
        {
            var entity =
                new PropertyMatchSalesAutomationRetryAlertHistory
                {
                    Id = Guid.NewGuid(),

                    AlertId = alertId,

                    AlertCode =
                        alertCode ?? string.Empty,

                    Action =
                        action ?? string.Empty,

                    PreviousStatus =
                        previousStatus,

                    NewStatus =
                        newStatus ?? string.Empty,

                    Severity =
                        severity ?? string.Empty,

                    PerformedBy =
                        performedBy,

                    Message =
                        message,

                    CreatedAt =
                        DateTime.UtcNow
                };

            await _dbContext
                .PropertyMatchSalesAutomationRetryAlertHistories
                .AddAsync(
                    entity,
                    cancellationToken);

            await _dbContext
                .SaveChangesAsync(
                    cancellationToken);

            return entity;
        }

        public async Task<List<
            PropertyMatchSalesAutomationRetryAlertHistory>>
            GetByAlertIdAsync(
                Guid alertId,
                int limit = 100,
                CancellationToken cancellationToken = default)
        {
            limit =
                Math.Clamp(
                    limit,
                    1,
                    1000);

            return await _dbContext
                .PropertyMatchSalesAutomationRetryAlertHistories
                .AsNoTracking()
                .Where(x =>
                    x.AlertId == alertId)
                .OrderByDescending(x =>
                    x.CreatedAt)
                .Take(limit)
                .ToListAsync(
                    cancellationToken);
        }

        public async Task<List<
            PropertyMatchSalesAutomationRetryAlertHistory>>
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
                .PropertyMatchSalesAutomationRetryAlertHistories
                .AsNoTracking()
                .OrderByDescending(x =>
                    x.CreatedAt)
                .Take(limit)
                .ToListAsync(
                    cancellationToken);
        }
    }
}
