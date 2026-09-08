using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Domain.Entities;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationOperationsReliabilityHistoryRepository
    {
        private readonly AppDbContext _dbContext;

        public PropertyCustomerMatchSalesAutomationOperationsReliabilityHistoryRepository(
            AppDbContext dbContext)
        {
            _dbContext =
                dbContext;
        }

        public async Task<
            PropertyMatchSalesAutomationOperationsReliabilityHistory>
            AddAsync(
                PropertyMatchSalesAutomationOperationsReliabilityScoreDto score,
                CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(score);

            var entity =
                new PropertyMatchSalesAutomationOperationsReliabilityHistory
                {
                    Id =
                        Guid.NewGuid(),

                    Score =
                        score.Score,

                    Grade =
                        score.Grade,

                    Status =
                        score.Status,

                    TotalPenalty =
                        score.TotalPenalty,

                    Bonus =
                        score.Bonus,

                    RiskScore =
                        score.RiskScore,

                    OpenIncidents =
                        score.OpenIncidents,

                    CriticalIncidents =
                        score.CriticalIncidents,

                    SlaBreaches =
                        score.SlaBreaches,

                    Level2Escalations =
                        score.Level2Escalations,

                    Level3Escalations =
                        score.Level3Escalations,

                    MttrMinutes =
                        score.MttrMinutes,

                    ResolutionRate =
                        score.ResolutionRate,

                    CreatedAt =
                        DateTime.UtcNow
                };

            await _dbContext
                .PropertyMatchSalesAutomationOperationsReliabilityHistories
                .AddAsync(
                    entity,
                    cancellationToken);

            await _dbContext
                .SaveChangesAsync(
                    cancellationToken);

            return entity;
        }

        public async Task<
            PropertyMatchSalesAutomationOperationsReliabilityHistory?>
            AddSnapshotIfNeededAsync(
                PropertyMatchSalesAutomationOperationsReliabilityScoreDto score,
                TimeSpan minimumInterval,
                decimal minimumScoreDifference = 1m,
                CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(score);

            var latest =
                await _dbContext
                    .PropertyMatchSalesAutomationOperationsReliabilityHistories
                    .AsNoTracking()
                    .OrderByDescending(x =>
                        x.CreatedAt)
                    .FirstOrDefaultAsync(
                        cancellationToken);

            if (latest == null)
            {
                return await AddAsync(
                    score,
                    cancellationToken);
            }

            var now =
                DateTime.UtcNow;

            var elapsed =
                now -
                latest.CreatedAt;

            var scoreDifference =
                Math.Abs(
                    score.Score -
                    latest.Score);

            var importantStateChanged =
                !string.Equals(
                    latest.Status,
                    score.Status,
                    StringComparison.OrdinalIgnoreCase)
                ||
                !string.Equals(
                    latest.Grade,
                    score.Grade,
                    StringComparison.OrdinalIgnoreCase)
                ||
                latest.SlaBreaches !=
                    score.SlaBreaches
                ||
                latest.Level3Escalations !=
                    score.Level3Escalations
                ||
                latest.CriticalIncidents !=
                    score.CriticalIncidents;

            var shouldCreate =
                elapsed >= minimumInterval
                ||
                scoreDifference >= minimumScoreDifference
                ||
                importantStateChanged;

            if (!shouldCreate)
            {
                return null;
            }

            return await AddAsync(
                score,
                cancellationToken);
        }

        public async Task<List<
            PropertyMatchSalesAutomationOperationsReliabilityHistory>>
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
                .PropertyMatchSalesAutomationOperationsReliabilityHistories
                .AsNoTracking()
                .OrderByDescending(x =>
                    x.CreatedAt)
                .Take(limit)
                .ToListAsync(
                    cancellationToken);
        }

        public async Task<List<
            PropertyMatchSalesAutomationOperationsReliabilityHistory>>
            GetSinceAsync(
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
                .PropertyMatchSalesAutomationOperationsReliabilityHistories
                .AsNoTracking()
                .Where(x =>
                    x.CreatedAt >= since)
                .OrderBy(x =>
                    x.CreatedAt)
                .Take(limit)
                .ToListAsync(
                    cancellationToken);
        }

        public async Task<
            PropertyMatchSalesAutomationOperationsReliabilityHistory?>
            GetLatestAsync(
                CancellationToken cancellationToken = default)
        {
            return await _dbContext
                .PropertyMatchSalesAutomationOperationsReliabilityHistories
                .AsNoTracking()
                .OrderByDescending(x =>
                    x.CreatedAt)
                .FirstOrDefaultAsync(
                    cancellationToken);
        }
    }
}
