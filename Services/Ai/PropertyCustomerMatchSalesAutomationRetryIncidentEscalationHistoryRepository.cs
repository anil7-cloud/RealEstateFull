using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Domain.Entities;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationRetryIncidentEscalationHistoryRepository
    {
        private readonly AppDbContext _dbContext;

        public PropertyCustomerMatchSalesAutomationRetryIncidentEscalationHistoryRepository(
            AppDbContext dbContext)
        {
            _dbContext =
                dbContext;
        }

        public async Task<
            PropertyMatchSalesAutomationRetryIncidentEscalationHistory>
            AddAsync(
                Guid incidentId,
                string incidentNumber,
                string action,
                string? previousLevel,
                string newLevel,
                int previousLevelNumber,
                int newLevelNumber,
                string severity,
                int overdueMinutes,
                string? performedBy = null,
                string? message = null,
                CancellationToken cancellationToken = default)
        {
            var history =
                new PropertyMatchSalesAutomationRetryIncidentEscalationHistory
                {
                    Id =
                        Guid.NewGuid(),

                    IncidentId =
                        incidentId,

                    IncidentNumber =
                        incidentNumber,

                    Action =
                        action,

                    PreviousLevel =
                        previousLevel,

                    NewLevel =
                        newLevel,

                    PreviousLevelNumber =
                        previousLevelNumber,

                    NewLevelNumber =
                        newLevelNumber,

                    Severity =
                        severity,

                    OverdueMinutes =
                        overdueMinutes,

                    PerformedBy =
                        performedBy,

                    Message =
                        message,

                    CreatedAt =
                        DateTime.UtcNow
                };

            await _dbContext
                .PropertyMatchSalesAutomationRetryIncidentEscalationHistories
                .AddAsync(
                    history,
                    cancellationToken);

            await _dbContext
                .SaveChangesAsync(
                    cancellationToken);

            return history;
        }

        public async Task<List<
            PropertyMatchSalesAutomationRetryIncidentEscalationHistory>>
            GetByIncidentIdAsync(
                Guid incidentId,
                CancellationToken cancellationToken = default)
        {
            return await _dbContext
                .PropertyMatchSalesAutomationRetryIncidentEscalationHistories
                .AsNoTracking()
                .Where(x =>
                    x.IncidentId == incidentId)
                .OrderBy(x =>
                    x.CreatedAt)
                .ToListAsync(
                    cancellationToken);
        }

        public async Task<List<
            PropertyMatchSalesAutomationRetryIncidentEscalationHistory>>
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
                .PropertyMatchSalesAutomationRetryIncidentEscalationHistories
                .AsNoTracking()
                .OrderByDescending(x =>
                    x.CreatedAt)
                .Take(limit)
                .ToListAsync(
                    cancellationToken);
        }
    }
}
