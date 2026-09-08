using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Domain.Entities;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertHistoryRepository
    {
        private readonly AppDbContext _dbContext;

        public PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertHistoryRepository(
            AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<
            PropertyMatchSalesAutomationOperationsReliabilityAlertHistory>
            AddAsync(
                Guid alertId,
                string alertNumber,
                string action,
                string? previousStatus,
                string newStatus,
                string severity,
                string priority,
                string? performedBy,
                string? message,
                CancellationToken cancellationToken = default)
        {
            var entity =
                new PropertyMatchSalesAutomationOperationsReliabilityAlertHistory
                {
                    Id = Guid.NewGuid(),

                    AlertId = alertId,

                    AlertNumber = alertNumber,

                    Action = action,

                    PreviousStatus = previousStatus,

                    NewStatus = newStatus,

                    Severity = severity,

                    Priority = priority,

                    PerformedBy = performedBy,

                    Message = message,

                    CreatedAt = DateTime.UtcNow
                };

            await _dbContext
                .PropertyMatchSalesAutomationOperationsReliabilityAlertHistories
                .AddAsync(
                    entity,
                    cancellationToken);

            await _dbContext
                .SaveChangesAsync(
                    cancellationToken);

            return entity;
        }

        public async Task<List<
            PropertyMatchSalesAutomationOperationsReliabilityAlertHistory>>
            GetByAlertIdAsync(
                Guid alertId,
                CancellationToken cancellationToken = default)
        {
            return await _dbContext
                .PropertyMatchSalesAutomationOperationsReliabilityAlertHistories
                .AsNoTracking()
                .Where(x =>
                    x.AlertId == alertId)
                .OrderBy(x =>
                    x.CreatedAt)
                .ToListAsync(
                    cancellationToken);
        }

        public async Task<List<
            PropertyMatchSalesAutomationOperationsReliabilityAlertHistory>>
            GetRecentAsync(
                int limit = 100,
                CancellationToken cancellationToken = default)
        {
            limit =
                Math.Clamp(
                    limit,
                    1,
                    5000);

            return await _dbContext
                .PropertyMatchSalesAutomationOperationsReliabilityAlertHistories
                .AsNoTracking()
                .OrderByDescending(x =>
                    x.CreatedAt)
                .Take(limit)
                .ToListAsync(
                    cancellationToken);
        }
    }
}
