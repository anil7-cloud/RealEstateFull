using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Domain.Entities;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationOperationsHealthIncidentRepository
    {
        private readonly AppDbContext _dbContext;

        public PropertyCustomerMatchSalesAutomationOperationsHealthIncidentRepository(
            AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<
            PropertyMatchSalesAutomationOperationsHealthIncident?>
            GetActiveAsync(
                CancellationToken cancellationToken = default)
        {
            return await _dbContext
                .PropertyMatchSalesAutomationOperationsHealthIncidents
                .FirstOrDefaultAsync(
                    x => x.Status == "Open",
                    cancellationToken);
        }

        public async Task<
            PropertyMatchSalesAutomationOperationsHealthIncident>
            CreateAsync(
                PropertyMatchSalesAutomationOperationsHealthTrendAnomalyDto anomaly,
                CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow;

            var entity =
                new PropertyMatchSalesAutomationOperationsHealthIncident
                {
                    Id =
                        Guid.NewGuid(),

                    IncidentNumber =
                        $"HI-{now:yyyyMMddHHmmss}-{Guid.NewGuid():N}"
                            .Substring(0, 24),

                    Severity =
                        anomaly.Severity,

                    Reason =
                        anomaly.Reason,

                    Status =
                        "Open",

                    FirstScore =
                        anomaly.FirstScore,

                    LatestScore =
                        anomaly.LatestScore,

                    PeakScore =
                        anomaly.PeakScore,

                    ScoreDrop =
                        anomaly.ScoreDrop,

                    SnapshotCount =
                        anomaly.SnapshotCount,

                    WindowStart =
                        anomaly.WindowStart,

                    WindowEnd =
                        anomaly.WindowEnd,

                    CreatedAt =
                        now
                };

            await _dbContext
                .PropertyMatchSalesAutomationOperationsHealthIncidents
                .AddAsync(
                    entity,
                    cancellationToken);

            await _dbContext
                .SaveChangesAsync(
                    cancellationToken);

            return entity;
        }

        public async Task UpdateAsync(
            PropertyMatchSalesAutomationOperationsHealthIncident incident,
            PropertyMatchSalesAutomationOperationsHealthTrendAnomalyDto anomaly,
            CancellationToken cancellationToken = default)
        {
            incident.Severity =
                anomaly.Severity;

            incident.Reason =
                anomaly.Reason;

            incident.LatestScore =
                anomaly.LatestScore;

            incident.PeakScore =
                anomaly.PeakScore;

            incident.ScoreDrop =
                anomaly.ScoreDrop;

            incident.SnapshotCount =
                anomaly.SnapshotCount;

            incident.WindowStart =
                anomaly.WindowStart;

            incident.WindowEnd =
                anomaly.WindowEnd;

            await _dbContext
                .SaveChangesAsync(
                    cancellationToken);
        }

        public async Task<bool> ResolveActiveAsync(
            CancellationToken cancellationToken = default)
        {
            var incident =
                await GetActiveAsync(
                    cancellationToken);

            if (incident == null)
            {
                return false;
            }

            incident.Status =
                "Resolved";

            incident.ResolvedAt =
                DateTime.UtcNow;

            await _dbContext
                .SaveChangesAsync(
                    cancellationToken);

            return true;
        }

        public async Task<List<
            PropertyMatchSalesAutomationOperationsHealthIncident>>
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
                .PropertyMatchSalesAutomationOperationsHealthIncidents
                .AsNoTracking()
                .Where(x =>
                    x.CreatedAt >= since)
                .OrderByDescending(x =>
                    x.CreatedAt)
                .Take(limit)
                .ToListAsync(
                    cancellationToken);
        }

        public async Task<List<
            PropertyMatchSalesAutomationOperationsHealthIncident>>
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
                .PropertyMatchSalesAutomationOperationsHealthIncidents
                .AsNoTracking()
                .OrderByDescending(x =>
                    x.CreatedAt)
                .Take(limit)
                .ToListAsync(
                    cancellationToken);
        }
    }
}
