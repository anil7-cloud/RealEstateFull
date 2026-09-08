using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Domain.Entities;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationRetryIncidentHistoryRepository
    {
        private readonly AppDbContext _dbContext;

        public PropertyCustomerMatchSalesAutomationRetryIncidentHistoryRepository(
            AppDbContext dbContext)
        {
            _dbContext =
                dbContext;
        }

        public async Task<PropertyMatchSalesAutomationRetryIncidentHistory>
            AddAsync(
                Guid incidentId,
                string incidentNumber,
                string action,
                string? previousStatus,
                string newStatus,
                string severity,
                decimal riskScore,
                string? performedBy = null,
                string? message = null,
                CancellationToken cancellationToken = default)
        {
            var history =
                new PropertyMatchSalesAutomationRetryIncidentHistory
                {
                    Id =
                        Guid.NewGuid(),

                    IncidentId =
                        incidentId,

                    IncidentNumber =
                        incidentNumber,

                    Action =
                        action,

                    PreviousStatus =
                        previousStatus,

                    NewStatus =
                        newStatus,

                    Severity =
                        severity,

                    RiskScore =
                        riskScore,

                    PerformedBy =
                        performedBy,

                    Message =
                        message,

                    CreatedAt =
                        DateTime.UtcNow
                };

            await _dbContext
                .PropertyMatchSalesAutomationRetryIncidentHistories
                .AddAsync(
                    history,
                    cancellationToken);

            await _dbContext
                .SaveChangesAsync(
                    cancellationToken);

            return history;
        }

        public async Task<List<
            PropertyMatchSalesAutomationRetryIncidentHistory>>
            GetByIncidentIdAsync(
                Guid incidentId,
                int limit = 100,
                CancellationToken cancellationToken = default)
        {
            limit =
                Math.Clamp(
                    limit,
                    1,
                    1000);

            return await _dbContext
                .PropertyMatchSalesAutomationRetryIncidentHistories
                .AsNoTracking()
                .Where(x =>
                    x.IncidentId == incidentId)
                .OrderByDescending(x =>
                    x.CreatedAt)
                .Take(limit)
                .ToListAsync(
                    cancellationToken);
        }

        public async Task<List<
            PropertyMatchSalesAutomationRetryIncidentHistory>>
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
                .PropertyMatchSalesAutomationRetryIncidentHistories
                .AsNoTracking()
                .OrderByDescending(x =>
                    x.CreatedAt)
                .Take(limit)
                .ToListAsync(
                    cancellationToken);
        }
    }
}
