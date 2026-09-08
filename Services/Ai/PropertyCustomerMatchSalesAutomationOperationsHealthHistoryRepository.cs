using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Domain.Entities;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationOperationsHealthHistoryRepository
    {
        private readonly AppDbContext _dbContext;

        public PropertyCustomerMatchSalesAutomationOperationsHealthHistoryRepository(
            AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<
            PropertyMatchSalesAutomationOperationsHealthSnapshot>
            CreateAsync(
                PropertyMatchSalesAutomationOperationsHealthScoreDto health,
                CancellationToken cancellationToken = default)
        {
            var entity =
                new PropertyMatchSalesAutomationOperationsHealthSnapshot
                {
                    Id = Guid.NewGuid(),

                    Score =
                        health.Score,

                    Status =
                        health.Status,

                    ReliabilityScore =
                        health.ReliabilityScore,

                    AnomalyPenalty =
                        health.AnomalyPenalty,

                    EscalationPenalty =
                        health.EscalationPenalty,

                    HasAnomaly =
                        health.HasAnomaly,

                    AnomalySeverity =
                        health.AnomalySeverity,

                    ActiveEscalations =
                        health.ActiveEscalations,

                    HighestEscalationLevel =
                        health.HighestEscalationLevel,

                    CreatedAt =
                        DateTime.UtcNow
                };

            await _dbContext
                .PropertyMatchSalesAutomationOperationsHealthSnapshots
                .AddAsync(
                    entity,
                    cancellationToken);

            await _dbContext
                .SaveChangesAsync(
                    cancellationToken);

            return entity;
        }

        public async Task<
            PropertyMatchSalesAutomationOperationsHealthSnapshot?>
            GetLatestAsync(
                CancellationToken cancellationToken = default)
        {
            return await _dbContext
                .PropertyMatchSalesAutomationOperationsHealthSnapshots
                .AsNoTracking()
                .OrderByDescending(x =>
                    x.CreatedAt)
                .FirstOrDefaultAsync(
                    cancellationToken);
        }

        public async Task<List<
            PropertyMatchSalesAutomationOperationsHealthSnapshot>>
            GetSinceAsync(
                DateTime since,
                int limit = 10000,
                CancellationToken cancellationToken = default)
        {
            limit =
                Math.Clamp(
                    limit,
                    1,
                    20000);

            return await _dbContext
                .PropertyMatchSalesAutomationOperationsHealthSnapshots
                .AsNoTracking()
                .Where(x =>
                    x.CreatedAt >= since)
                .OrderBy(x =>
                    x.CreatedAt)
                .Take(limit)
                .ToListAsync(
                    cancellationToken);
        }
    }
}
