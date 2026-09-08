using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationRetryEventCleanupWorker
        : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        private readonly ILogger<
            PropertyCustomerMatchSalesAutomationRetryEventCleanupWorker>
            _logger;

        private static readonly TimeSpan CleanupInterval =
            TimeSpan.FromHours(24);

        private const int RetentionDays = 90;

        public PropertyCustomerMatchSalesAutomationRetryEventCleanupWorker(
            IServiceScopeFactory scopeFactory,
            ILogger<
                PropertyCustomerMatchSalesAutomationRetryEventCleanupWorker>
                logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                "Retry event cleanup worker started. RetentionDays={RetentionDays}",
                RetentionDays);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await CleanupAsync(
                        stoppingToken);
                }
                catch (OperationCanceledException)
                    when (stoppingToken.IsCancellationRequested)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(
                        ex,
                        "Retry event cleanup cycle failed.");
                }

                try
                {
                    await Task.Delay(
                        CleanupInterval,
                        stoppingToken);
                }
                catch (OperationCanceledException)
                    when (stoppingToken.IsCancellationRequested)
                {
                    break;
                }
            }

            _logger.LogInformation(
                "Retry event cleanup worker stopped.");
        }

        private async Task CleanupAsync(
            CancellationToken cancellationToken)
        {
            using var scope =
                _scopeFactory.CreateScope();

            var repository =
                scope.ServiceProvider
                    .GetRequiredService<
                        PropertyCustomerMatchSalesAutomationRetryEventRepository>();

            var cutoff =
                DateTime.UtcNow.AddDays(
                    -RetentionDays);

            var deleted =
                await repository
                    .DeleteOlderThanAsync(
                        cutoff,
                        cancellationToken);

            if (deleted > 0)
            {
                _logger.LogInformation(
                    "Deleted {DeletedCount} retry events older than {Cutoff}.",
                    deleted,
                    cutoff);
            }
        }
    }
}
