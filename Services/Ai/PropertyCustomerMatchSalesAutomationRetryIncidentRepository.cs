using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Domain.Entities;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationRetryIncidentRepository
    {
        private readonly AppDbContext _dbContext;

        private readonly
            PropertyCustomerMatchSalesAutomationRetryIncidentHistoryRepository
                _historyRepository;

        private readonly
            PropertyCustomerMatchSalesAutomationRetryIncidentEscalationRepository
                _escalationRepository;

        public PropertyCustomerMatchSalesAutomationRetryIncidentRepository(
            AppDbContext dbContext,
            PropertyCustomerMatchSalesAutomationRetryIncidentHistoryRepository historyRepository,
            PropertyCustomerMatchSalesAutomationRetryIncidentEscalationRepository escalationRepository)
        {
            _dbContext =
                dbContext;

            _historyRepository =
                historyRepository;

            _escalationRepository =
                escalationRepository;
        }

        public async Task<PropertyMatchSalesAutomationRetryIncident>
            CreateAsync(
                PropertyMatchSalesAutomationRetryIncident incident,
                CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(incident);

            if (incident.Id == Guid.Empty)
                incident.Id = Guid.NewGuid();

            if (string.IsNullOrWhiteSpace(
                    incident.IncidentNumber))
            {
                incident.IncidentNumber =
                    $"RETRY-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid():N}"
                        [..30]
                        .ToUpperInvariant();
            }

            incident.Status =
                "Open";

            incident.CreatedAt =
                DateTime.UtcNow;

            incident.UpdatedAt =
                DateTime.UtcNow;

            await _dbContext
                .PropertyMatchSalesAutomationRetryIncidents
                .AddAsync(
                    incident,
                    cancellationToken);

            await _dbContext
                .SaveChangesAsync(
                    cancellationToken);

            await _historyRepository
                .AddAsync(
                    incident.Id,
                    incident.IncidentNumber,
                    "Created",
                    null,
                    incident.Status,
                    incident.Severity,
                    incident.RiskScore,
                    "System",
                    incident.Description,
                    cancellationToken);

            return incident;
        }

        public async Task<PropertyMatchSalesAutomationRetryIncident?>
            GetOpenIncidentAsync(
                CancellationToken cancellationToken = default)
        {
            return await _dbContext
                .PropertyMatchSalesAutomationRetryIncidents
                .FirstOrDefaultAsync(
                    x =>
                        x.Status == "Open" ||
                        x.Status == "Acknowledged",
                    cancellationToken);
        }

        public async Task<List<PropertyMatchSalesAutomationRetryIncident>>
            GetCreatedSinceAsync(
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
                .PropertyMatchSalesAutomationRetryIncidents
                .AsNoTracking()
                .Where(x =>
                    x.CreatedAt >= since)
                .OrderByDescending(x =>
                    x.CreatedAt)
                .Take(limit)
                .ToListAsync(
                    cancellationToken);
        }

        public async Task<List<PropertyMatchSalesAutomationRetryIncident>>
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
                .PropertyMatchSalesAutomationRetryIncidents
                .AsNoTracking()
                .OrderByDescending(x =>
                    x.CreatedAt)
                .Take(limit)
                .ToListAsync(
                    cancellationToken);
        }

        public async Task<PropertyMatchSalesAutomationRetryIncident?>
            AcknowledgeAsync(
                Guid id,
                string? assignedTo,
                CancellationToken cancellationToken = default)
        {
            var incident =
                await _dbContext
                    .PropertyMatchSalesAutomationRetryIncidents
                    .FirstOrDefaultAsync(
                        x => x.Id == id,
                        cancellationToken);

            if (incident == null ||
                incident.Status == "Resolved")
            {
                return incident;
            }

            var previousStatus =
                incident.Status;

            incident.Status =
                "Acknowledged";

            incident.AssignedTo =
                assignedTo;

            incident.AcknowledgedAt =
                DateTime.UtcNow;

            incident.UpdatedAt =
                DateTime.UtcNow;

            await _dbContext
                .SaveChangesAsync(
                    cancellationToken);

            await _historyRepository
                .AddAsync(
                    incident.Id,
                    incident.IncidentNumber,
                    "Acknowledged",
                    previousStatus,
                    incident.Status,
                    incident.Severity,
                    incident.RiskScore,
                    incident.AssignedTo,
                    "Incident acknowledged.",
                    cancellationToken);

            return incident;
        }

        public async Task<PropertyMatchSalesAutomationRetryIncident?>
            ResolveAsync(
                Guid id,
                string? resolvedBy,
                CancellationToken cancellationToken = default)
        {
            var incident =
                await _dbContext
                    .PropertyMatchSalesAutomationRetryIncidents
                    .FirstOrDefaultAsync(
                        x => x.Id == id,
                        cancellationToken);

            if (incident == null)
                return null;

            var previousResolveStatus =
                incident.Status;

            incident.Status =
                "Resolved";

            incident.ResolvedBy =
                resolvedBy;

            incident.ResolvedAt =
                DateTime.UtcNow;

            incident.UpdatedAt =
                DateTime.UtcNow;

            await _dbContext
                .SaveChangesAsync(
                    cancellationToken);

            await _historyRepository
                .AddAsync(
                    incident.Id,
                    incident.IncidentNumber,
                    "Resolved",
                    previousResolveStatus,
                    incident.Status,
                    incident.Severity,
                    incident.RiskScore,
                    incident.ResolvedBy,
                    "Incident resolved.",
                    cancellationToken);

            await _escalationRepository
                .ResolveAsync(
                    incident.Id,
                    cancellationToken);

            return incident;
        }
    }
}
