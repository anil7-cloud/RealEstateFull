using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Domain.Entities;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertSlaBreachRepository
    {
        private readonly AppDbContext _dbContext;

        public PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertSlaBreachRepository(
            AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<
            PropertyMatchSalesAutomationOperationsReliabilityAlertSlaBreach?>
            AddIfNeededAsync(
                PropertyMatchSalesAutomationReliabilityAlertSlaDto item,
                string breachType,
                decimal limitMinutes,
                decimal actualMinutes,
                CancellationToken cancellationToken = default)
        {
            var existing =
                await _dbContext
                    .PropertyMatchSalesAutomationOperationsReliabilityAlertSlaBreaches
                    .FirstOrDefaultAsync(
                        x =>
                            x.AlertId == item.AlertId &&
                            x.BreachType == breachType &&
                            x.Status == "Active",
                        cancellationToken);

            if (existing != null)
            {
                existing.ActualMinutes =
                    actualMinutes;

                existing.OverdueMinutes =
                    Math.Max(
                        0m,
                        actualMinutes - limitMinutes);

                await _dbContext
                    .SaveChangesAsync(
                        cancellationToken);

                return null;
            }

            var entity =
                new PropertyMatchSalesAutomationOperationsReliabilityAlertSlaBreach
                {
                    Id = Guid.NewGuid(),

                    AlertId =
                        item.AlertId,

                    AlertNumber =
                        item.AlertNumber,

                    BreachType =
                        breachType,

                    Priority =
                        item.Priority,

                    Severity =
                        item.Severity,

                    LimitMinutes =
                        limitMinutes,

                    ActualMinutes =
                        actualMinutes,

                    OverdueMinutes =
                        Math.Max(
                            0m,
                            actualMinutes - limitMinutes),

                    Status =
                        "Active",

                    DetectedAt =
                        DateTime.UtcNow
                };

            await _dbContext
                .PropertyMatchSalesAutomationOperationsReliabilityAlertSlaBreaches
                .AddAsync(
                    entity,
                    cancellationToken);

            await _dbContext
                .SaveChangesAsync(
                    cancellationToken);

            return entity;
        }

        public async Task<int>
            ResolveByAlertAsync(
                Guid alertId,
                CancellationToken cancellationToken = default)
        {
            var breaches =
                await _dbContext
                    .PropertyMatchSalesAutomationOperationsReliabilityAlertSlaBreaches
                    .Where(x =>
                        x.AlertId == alertId &&
                        x.Status == "Active")
                    .ToListAsync(
                        cancellationToken);

            if (breaches.Count == 0)
            {
                return 0;
            }

            var now =
                DateTime.UtcNow;

            foreach (var breach in breaches)
            {
                breach.Status =
                    "Resolved";

                breach.ResolvedAt =
                    now;
            }

            await _dbContext
                .SaveChangesAsync(
                    cancellationToken);

            return breaches.Count;
        }

        public async Task<List<
            PropertyMatchSalesAutomationOperationsReliabilityAlertSlaBreach>>
            GetSinceAsync(
                DateTime since,
                int limit = 5000,
                CancellationToken cancellationToken = default)
        {
            limit = Math.Clamp(limit, 1, 10000);

            return await _dbContext
                .PropertyMatchSalesAutomationOperationsReliabilityAlertSlaBreaches
                .AsNoTracking()
                .Where(x => x.DetectedAt >= since)
                .OrderByDescending(x => x.DetectedAt)
                .Take(limit)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<
            PropertyMatchSalesAutomationOperationsReliabilityAlertSlaBreach>>
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
                .PropertyMatchSalesAutomationOperationsReliabilityAlertSlaBreaches
                .AsNoTracking()
                .Where(x =>
                    x.Status == "Active")
                .OrderByDescending(x =>
                    x.Priority == "P1")
                .ThenByDescending(x =>
                    x.OverdueMinutes)
                .Take(limit)
                .ToListAsync(
                    cancellationToken);
        }

        public async Task<List<
            PropertyMatchSalesAutomationOperationsReliabilityAlertSlaBreach>>
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
                .PropertyMatchSalesAutomationOperationsReliabilityAlertSlaBreaches
                .AsNoTracking()
                .OrderByDescending(x =>
                    x.DetectedAt)
                .Take(limit)
                .ToListAsync(
                    cancellationToken);
        }
    }
}
