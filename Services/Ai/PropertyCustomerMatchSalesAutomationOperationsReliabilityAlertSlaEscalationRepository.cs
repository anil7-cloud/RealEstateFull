using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Domain.Entities;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertSlaEscalationRepository
    {
        private readonly AppDbContext _dbContext;

        public PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertSlaEscalationRepository(
            AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ExistsAsync(
            Guid breachId,
            string escalationLevel,
            CancellationToken cancellationToken = default)
        {
            return await _dbContext
                .PropertyMatchSalesAutomationOperationsReliabilityAlertSlaEscalations
                .AnyAsync(
                    x =>
                        x.BreachId == breachId &&
                        x.EscalationLevel == escalationLevel,
                    cancellationToken);
        }

        public async Task<
            PropertyMatchSalesAutomationOperationsReliabilityAlertSlaEscalation?>
            CreateIfNeededAsync(
                PropertyMatchSalesAutomationOperationsReliabilityAlertSlaBreach breach,
                string escalationLevel,
                CancellationToken cancellationToken = default)
        {
            var exists =
                await ExistsAsync(
                    breach.Id,
                    escalationLevel,
                    cancellationToken);

            if (exists)
            {
                return null;
            }

            var entity =
                new PropertyMatchSalesAutomationOperationsReliabilityAlertSlaEscalation
                {
                    Id = Guid.NewGuid(),

                    BreachId =
                        breach.Id,

                    AlertId =
                        breach.AlertId,

                    AlertNumber =
                        breach.AlertNumber,

                    BreachType =
                        breach.BreachType,

                    Priority =
                        breach.Priority,

                    EscalationLevel =
                        escalationLevel,

                    OverdueMinutes =
                        breach.OverdueMinutes,

                    Status =
                        "Active",

                    EscalatedAt =
                        DateTime.UtcNow
                };

            await _dbContext
                .PropertyMatchSalesAutomationOperationsReliabilityAlertSlaEscalations
                .AddAsync(
                    entity,
                    cancellationToken);

            await _dbContext
                .SaveChangesAsync(
                    cancellationToken);

            return entity;
        }

        public async Task<int>
            ResolveInactiveBreachesAsync(
                CancellationToken cancellationToken = default)
        {
            var activeEscalations =
                await _dbContext
                    .PropertyMatchSalesAutomationOperationsReliabilityAlertSlaEscalations
                    .Where(x =>
                        x.Status == "Active")
                    .ToListAsync(
                        cancellationToken);

            if (activeEscalations.Count == 0)
            {
                return 0;
            }

            var breachIds =
                activeEscalations
                    .Select(x => x.BreachId)
                    .Distinct()
                    .ToList();

            var activeBreachIds =
                await _dbContext
                    .PropertyMatchSalesAutomationOperationsReliabilityAlertSlaBreaches
                    .AsNoTracking()
                    .Where(x =>
                        breachIds.Contains(x.Id) &&
                        x.Status == "Active")
                    .Select(x => x.Id)
                    .ToListAsync(
                        cancellationToken);

            var activeSet =
                activeBreachIds.ToHashSet();

            var toResolve =
                activeEscalations
                    .Where(x =>
                        !activeSet.Contains(
                            x.BreachId))
                    .ToList();

            if (toResolve.Count == 0)
            {
                return 0;
            }

            var now =
                DateTime.UtcNow;

            foreach (var escalation in toResolve)
            {
                escalation.Status =
                    "Resolved";

                escalation.ResolvedAt =
                    now;
            }

            await _dbContext
                .SaveChangesAsync(
                    cancellationToken);

            return toResolve.Count;
        }

        public async Task<int> ResolveByBreachAsync(
            Guid breachId,
            CancellationToken cancellationToken = default)
        {
            var items =
                await _dbContext
                    .PropertyMatchSalesAutomationOperationsReliabilityAlertSlaEscalations
                    .Where(x =>
                        x.BreachId == breachId &&
                        x.Status == "Active")
                    .ToListAsync(
                        cancellationToken);

            if (items.Count == 0)
            {
                return 0;
            }

            var now = DateTime.UtcNow;

            foreach (var item in items)
            {
                item.Status = "Resolved";
                item.ResolvedAt = now;
            }

            await _dbContext
                .SaveChangesAsync(
                    cancellationToken);

            return items.Count;
        }

        public async Task<List<
            PropertyMatchSalesAutomationOperationsReliabilityAlertSlaEscalation>>
            GetSinceAsync(
                DateTime since,
                int limit = 5000,
                CancellationToken cancellationToken = default)
        {
            limit = Math.Clamp(limit, 1, 10000);

            return await _dbContext
                .PropertyMatchSalesAutomationOperationsReliabilityAlertSlaEscalations
                .AsNoTracking()
                .Where(x => x.EscalatedAt >= since)
                .OrderByDescending(x => x.EscalatedAt)
                .Take(limit)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<
            PropertyMatchSalesAutomationOperationsReliabilityAlertSlaEscalation>>
            GetActiveAsync(
                int limit = 100,
                CancellationToken cancellationToken = default)
        {
            limit = Math.Clamp(limit, 1, 1000);

            return await _dbContext
                .PropertyMatchSalesAutomationOperationsReliabilityAlertSlaEscalations
                .AsNoTracking()
                .Where(x => x.Status == "Active")
                .OrderByDescending(x =>
                    x.EscalationLevel == "L3")
                .ThenByDescending(x =>
                    x.EscalationLevel == "L2")
                .ThenByDescending(x =>
                    x.EscalatedAt)
                .Take(limit)
                .ToListAsync(
                    cancellationToken);
        }

        public async Task<List<
            PropertyMatchSalesAutomationOperationsReliabilityAlertSlaEscalation>>
            GetRecentAsync(
                int limit = 100,
                CancellationToken cancellationToken = default)
        {
            limit = Math.Clamp(limit, 1, 5000);

            return await _dbContext
                .PropertyMatchSalesAutomationOperationsReliabilityAlertSlaEscalations
                .AsNoTracking()
                .OrderByDescending(x =>
                    x.EscalatedAt)
                .Take(limit)
                .ToListAsync(
                    cancellationToken);
        }
    }
}
