using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Domain.Entities;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationRetryEventRepository
    {
        private readonly AppDbContext _dbContext;

        public PropertyCustomerMatchSalesAutomationRetryEventRepository(
            AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PropertyMatchSalesAutomationRetryEvent>
            AddAsync(
                PropertyMatchSalesAutomationRetryEvent entity,
                CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(entity);

            if (entity.Id == Guid.Empty)
            {
                entity.Id =
                    Guid.NewGuid();
            }

            if (entity.CreatedAt == default)
            {
                entity.CreatedAt =
                    DateTime.UtcNow;
            }

            await _dbContext
                .PropertyMatchSalesAutomationRetryEvents
                .AddAsync(
                    entity,
                    cancellationToken);

            await _dbContext
                .SaveChangesAsync(
                    cancellationToken);

            return entity;
        }

        public async Task<List<PropertyMatchSalesAutomationRetryEvent>>
            GetByJobIdAsync(
                Guid jobId,
                int limit = 100,
                CancellationToken cancellationToken = default)
        {
            limit =
                Math.Clamp(
                    limit,
                    1,
                    1000);

            return await _dbContext
                .PropertyMatchSalesAutomationRetryEvents
                .AsNoTracking()
                .Where(x =>
                    x.JobId == jobId)
                .OrderByDescending(x =>
                    x.CreatedAt)
                .Take(limit)
                .ToListAsync(
                    cancellationToken);
        }

        public async Task<List<PropertyMatchSalesAutomationRetryEvent>>
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
                .PropertyMatchSalesAutomationRetryEvents
                .AsNoTracking()
                .OrderByDescending(x =>
                    x.CreatedAt)
                .Take(limit)
                .ToListAsync(
                    cancellationToken);
        }

        public async Task<List<PropertyMatchSalesAutomationRetryEvent>>
            GetByEventTypeAsync(
                string eventType,
                int limit = 100,
                CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(eventType))
            {
                return new();
            }

            limit =
                Math.Clamp(
                    limit,
                    1,
                    1000);

            return await _dbContext
                .PropertyMatchSalesAutomationRetryEvents
                .AsNoTracking()
                .Where(x =>
                    x.EventType == eventType)
                .OrderByDescending(x =>
                    x.CreatedAt)
                .Take(limit)
                .ToListAsync(
                    cancellationToken);
        }

        public async Task<
            PropertyMatchSalesAutomationRetryEventSummaryDto>
            GetSummaryAsync(
                CancellationToken cancellationToken = default)
        {
            var events =
                await _dbContext
                    .PropertyMatchSalesAutomationRetryEvents
                    .AsNoTracking()
                    .ToListAsync(
                        cancellationToken);

            var attempts =
                events.Count(x =>
                    x.EventType == "RetryAttempted");

            var succeeded =
                events.Count(x =>
                    x.EventType == "RetrySucceeded");

            var failed =
                events.Count(x =>
                    x.EventType == "RetryFailed");

            return new()
            {
                TotalEvents =
                    events.Count,

                RetryAttempts =
                    attempts,

                RetrySucceeded =
                    succeeded,

                RetryFailed =
                    failed,

                RetryCancelled =
                    events.Count(x =>
                        x.EventType == "RetryCancelled"),

                ClaimAcquired =
                    events.Count(x =>
                        x.EventType == "ClaimAcquired"),

                ClaimSkipped =
                    events.Count(x =>
                        x.EventType == "ClaimSkipped"),

                LeaseLost =
                    events.Count(x =>
                        x.EventType == "LeaseLost"),

                SuccessRate =
                    attempts == 0
                        ? 0
                        : Math.Round(
                            (decimal)succeeded /
                            attempts * 100m,
                            2),

                FailureRate =
                    attempts == 0
                        ? 0
                        : Math.Round(
                            (decimal)failed /
                            attempts * 100m,
                            2),

                GeneratedAt =
                    DateTime.UtcNow
            };
        }

        public async Task<int>
            DeleteOlderThanAsync(
                DateTime cutoff,
                CancellationToken cancellationToken = default)
        {
            return await _dbContext
                .PropertyMatchSalesAutomationRetryEvents
                .Where(x =>
                    x.CreatedAt < cutoff)
                .ExecuteDeleteAsync(
                    cancellationToken);
        }

        public async Task<bool>
            CanConnectAsync(
                CancellationToken cancellationToken = default)
        {
            try
            {
                return await _dbContext
                    .Database
                    .CanConnectAsync(
                        cancellationToken);
            }
            catch
            {
                return false;
            }
        }
    }

    public class PropertyMatchSalesAutomationRetryEventSummaryDto
    {
        public int TotalEvents { get; set; }

        public int RetryAttempts { get; set; }

        public int RetrySucceeded { get; set; }

        public int RetryFailed { get; set; }

        public int RetryCancelled { get; set; }

        public int ClaimAcquired { get; set; }

        public int ClaimSkipped { get; set; }

        public int LeaseLost { get; set; }

        public decimal SuccessRate { get; set; }

        public decimal FailureRate { get; set; }

        public DateTime GeneratedAt { get; set; }
    }
}
