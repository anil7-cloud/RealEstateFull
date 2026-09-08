using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Domain.Entities;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationAuditRepository
    {
        private readonly AppDbContext _dbContext;

        public PropertyCustomerMatchSalesAutomationAuditRepository(
            AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PropertyMatchSalesAutomationAudit>
            AddAsync(
                PropertyMatchSalesAutomationAudit audit,
                CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(audit);

            if (audit.AuditId == Guid.Empty)
                audit.AuditId = Guid.NewGuid();

            if (audit.CreatedAt == default)
                audit.CreatedAt = DateTime.UtcNow;

            await _dbContext
                .PropertyMatchSalesAutomationAudits
                .AddAsync(
                    audit,
                    cancellationToken);

            await _dbContext
                .SaveChangesAsync(
                    cancellationToken);

            return audit;
        }

        public async Task<PropertyMatchSalesAutomationAudit?>
            GetByAuditIdAsync(
                Guid auditId,
                CancellationToken cancellationToken = default)
        {
            return await _dbContext
                .PropertyMatchSalesAutomationAudits
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x => x.AuditId == auditId,
                    cancellationToken);
        }

        public async Task<List<PropertyMatchSalesAutomationAudit>>
            GetRecentAsync(
                int limit = 100,
                CancellationToken cancellationToken = default)
        {
            limit = NormalizeLimit(limit);

            return await _dbContext
                .PropertyMatchSalesAutomationAudits
                .AsNoTracking()
                .OrderByDescending(x => x.CreatedAt)
                .Take(limit)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<PropertyMatchSalesAutomationAudit>>
            GetForMatchAsync(
                int matchId,
                int limit = 100,
                CancellationToken cancellationToken = default)
        {
            if (matchId <= 0)
                throw new ArgumentOutOfRangeException(nameof(matchId));

            return await _dbContext
                .PropertyMatchSalesAutomationAudits
                .AsNoTracking()
                .Where(x => x.MatchId == matchId)
                .OrderByDescending(x => x.CreatedAt)
                .Take(NormalizeLimit(limit))
                .ToListAsync(cancellationToken);
        }

        public async Task<List<PropertyMatchSalesAutomationAudit>>
            GetForLeadAsync(
                int leadId,
                int limit = 100,
                CancellationToken cancellationToken = default)
        {
            if (leadId <= 0)
                throw new ArgumentOutOfRangeException(nameof(leadId));

            return await _dbContext
                .PropertyMatchSalesAutomationAudits
                .AsNoTracking()
                .Where(x => x.LeadId == leadId)
                .OrderByDescending(x => x.CreatedAt)
                .Take(NormalizeLimit(limit))
                .ToListAsync(cancellationToken);
        }

        public async Task<List<PropertyMatchSalesAutomationAudit>>
            GetForExecutionAsync(
                Guid executionId,
                int limit = 100,
                CancellationToken cancellationToken = default)
        {
            return await _dbContext
                .PropertyMatchSalesAutomationAudits
                .AsNoTracking()
                .Where(x =>
                    x.ExecutionId == executionId)
                .OrderByDescending(x => x.CreatedAt)
                .Take(NormalizeLimit(limit))
                .ToListAsync(cancellationToken);
        }

        public async Task<List<PropertyMatchSalesAutomationAudit>>
            GetFailuresAsync(
                int limit = 100,
                CancellationToken cancellationToken = default)
        {
            return await _dbContext
                .PropertyMatchSalesAutomationAudits
                .AsNoTracking()
                .Where(x =>
                    !x.IsSuccessful ||
                    x.Status == "Failed")
                .OrderByDescending(x => x.CreatedAt)
                .Take(NormalizeLimit(limit))
                .ToListAsync(cancellationToken);
        }

        public async Task<int>
            CountAsync(
                CancellationToken cancellationToken = default)
        {
            return await _dbContext
                .PropertyMatchSalesAutomationAudits
                .CountAsync(cancellationToken);
        }

        public async Task<int>
            CountFailuresAsync(
                CancellationToken cancellationToken = default)
        {
            return await _dbContext
                .PropertyMatchSalesAutomationAudits
                .CountAsync(
                    x =>
                        !x.IsSuccessful ||
                        x.Status == "Failed",
                    cancellationToken);
        }

        public async Task<int>
            CountSinceAsync(
                DateTime since,
                CancellationToken cancellationToken = default)
        {
            return await _dbContext
                .PropertyMatchSalesAutomationAudits
                .CountAsync(
                    x => x.CreatedAt >= since,
                    cancellationToken);
        }

        public async Task<bool>
            DeleteAsync(
                Guid auditId,
                CancellationToken cancellationToken = default)
        {
            var entity =
                await _dbContext
                    .PropertyMatchSalesAutomationAudits
                    .FirstOrDefaultAsync(
                        x => x.AuditId == auditId,
                        cancellationToken);

            if (entity == null)
                return false;

            _dbContext
                .PropertyMatchSalesAutomationAudits
                .Remove(entity);

            await _dbContext
                .SaveChangesAsync(
                    cancellationToken);

            return true;
        }

        private static int NormalizeLimit(int limit)
        {
            if (limit < 1)
                return 10;

            return Math.Min(limit, 500);
        }
    }
}
