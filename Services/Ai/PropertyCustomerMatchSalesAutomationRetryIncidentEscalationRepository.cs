using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Domain.Entities;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationRetryIncidentEscalationRepository
    {
        private readonly AppDbContext _dbContext;

        private readonly
            PropertyCustomerMatchSalesAutomationRetryIncidentEscalationHistoryRepository
                _historyRepository;

        public PropertyCustomerMatchSalesAutomationRetryIncidentEscalationRepository(
            AppDbContext dbContext,
            PropertyCustomerMatchSalesAutomationRetryIncidentEscalationHistoryRepository historyRepository)
        {
            _dbContext =
                dbContext;

            _historyRepository =
                historyRepository;
        }

        public async Task<
            PropertyMatchSalesAutomationRetryIncidentEscalation?>
            GetActiveByIncidentIdAsync(
                Guid incidentId,
                CancellationToken cancellationToken = default)
        {
            return await _dbContext
                .PropertyMatchSalesAutomationRetryIncidentEscalations
                .FirstOrDefaultAsync(
                    x =>
                        x.IncidentId == incidentId &&
                        x.Status == "Active",
                    cancellationToken);
        }

        public async Task<
            PropertyMatchSalesAutomationRetryIncidentEscalation>
            UpsertAsync(
                PropertyMatchSalesAutomationRetryIncidentEscalation entity,
                CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(entity);

            var existing =
                await GetActiveByIncidentIdAsync(
                    entity.IncidentId,
                    cancellationToken);

            if (existing == null)
            {
                if (entity.Id == Guid.Empty)
                {
                    entity.Id =
                        Guid.NewGuid();
                }

                entity.Status =
                    "Active";

                entity.CreatedAt =
                    DateTime.UtcNow;

                entity.UpdatedAt =
                    DateTime.UtcNow;

                await _dbContext
                    .PropertyMatchSalesAutomationRetryIncidentEscalations
                    .AddAsync(
                        entity,
                        cancellationToken);

                await _dbContext
                    .SaveChangesAsync(
                        cancellationToken);

                await _historyRepository
                    .AddAsync(
                        entity.IncidentId,
                        entity.IncidentNumber,
                        "EscalationCreated",
                        null,
                        entity.Level,
                        0,
                        entity.LevelNumber,
                        entity.Severity,
                        entity.OverdueMinutes,
                        entity.CreatedBy ?? "System",
                        $"Escalation created at {entity.Level}.",
                        cancellationToken);

                return entity;
            }

            /*
             * Escalation seviyesi geriye düşürülmez.
             * Level3 olmuş bir incident daha sonra Level1 olarak
             * overwrite edilmez.
             */
            var previousLevel =
                existing.Level;

            var previousLevelNumber =
                existing.LevelNumber;

            var levelRaised =
                entity.LevelNumber >
                existing.LevelNumber;

            if (levelRaised)
            {
                existing.Level =
                    entity.Level;

                existing.LevelNumber =
                    entity.LevelNumber;
            }

            existing.OverdueMinutes =
                Math.Max(
                    existing.OverdueMinutes,
                    entity.OverdueMinutes);

            existing.Severity =
                entity.Severity;

            existing.RecommendedAction =
                entity.RecommendedAction;

            existing.UpdatedAt =
                DateTime.UtcNow;

            await _dbContext
                .SaveChangesAsync(
                    cancellationToken);

            if (levelRaised)
            {
                await _historyRepository
                    .AddAsync(
                        existing.IncidentId,
                        existing.IncidentNumber,
                        "EscalationRaised",
                        previousLevel,
                        existing.Level,
                        previousLevelNumber,
                        existing.LevelNumber,
                        existing.Severity,
                        existing.OverdueMinutes,
                        "System",
                        $"Escalation raised from {previousLevel} to {existing.Level}.",
                        cancellationToken);
            }

            return existing;
        }

        public async Task<List<
            PropertyMatchSalesAutomationRetryIncidentEscalation>>
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
                .PropertyMatchSalesAutomationRetryIncidentEscalations
                .AsNoTracking()
                .Where(x =>
                    x.Status == "Active")
                .OrderByDescending(x =>
                    x.LevelNumber)
                .ThenByDescending(x =>
                    x.OverdueMinutes)
                .Take(limit)
                .ToListAsync(
                    cancellationToken);
        }

        public async Task<List<
            PropertyMatchSalesAutomationRetryIncidentEscalation>>
            GetByIncidentIdAsync(
                Guid incidentId,
                CancellationToken cancellationToken = default)
        {
            return await _dbContext
                .PropertyMatchSalesAutomationRetryIncidentEscalations
                .AsNoTracking()
                .Where(x =>
                    x.IncidentId == incidentId)
                .OrderByDescending(x =>
                    x.CreatedAt)
                .ToListAsync(
                    cancellationToken);
        }

        public async Task<bool>
            ResolveAsync(
                Guid incidentId,
                CancellationToken cancellationToken = default)
        {
            var entity =
                await GetActiveByIncidentIdAsync(
                    incidentId,
                    cancellationToken);

            if (entity == null)
            {
                return false;
            }

            entity.Status =
                "Resolved";

            entity.ResolvedAt =
                DateTime.UtcNow;

            entity.UpdatedAt =
                DateTime.UtcNow;

            await _dbContext
                .SaveChangesAsync(
                    cancellationToken);

            await _historyRepository
                .AddAsync(
                    entity.IncidentId,
                    entity.IncidentNumber,
                    "EscalationResolved",
                    entity.Level,
                    "Resolved",
                    entity.LevelNumber,
                    0,
                    entity.Severity,
                    entity.OverdueMinutes,
                    "System",
                    $"Escalation resolved from {entity.Level}.",
                    cancellationToken);

            return true;
        }
    }
}
