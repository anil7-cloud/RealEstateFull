namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationOperationsHealthHistoryService
    {
        private readonly
            PropertyCustomerMatchSalesAutomationOperationsHealthScoreService
                _healthScoreService;

        private readonly
            PropertyCustomerMatchSalesAutomationOperationsHealthHistoryRepository
                _repository;

        public PropertyCustomerMatchSalesAutomationOperationsHealthHistoryService(
            PropertyCustomerMatchSalesAutomationOperationsHealthScoreService healthScoreService,
            PropertyCustomerMatchSalesAutomationOperationsHealthHistoryRepository repository)
        {
            _healthScoreService =
                healthScoreService;

            _repository =
                repository;
        }

        public async Task<
            PropertyMatchSalesAutomationOperationsHealthSnapshotResultDto>
            CaptureAsync(
                CancellationToken cancellationToken = default)
        {
            var latest =
                await _repository
                    .GetLatestAsync(
                        cancellationToken);

            if (latest != null &&
                DateTime.UtcNow - latest.CreatedAt <
                TimeSpan.FromMinutes(5))
            {
                return new()
                {
                    Created = false,
                    SnapshotId = latest.Id,
                    Score = latest.Score,
                    Status = latest.Status,
                    CreatedAt = latest.CreatedAt
                };
            }

            var health =
                await _healthScoreService
                    .GetAsync(
                        cancellationToken);

            var snapshot =
                await _repository
                    .CreateAsync(
                        health,
                        cancellationToken);

            return new()
            {
                Created = true,
                SnapshotId = snapshot.Id,
                Score = snapshot.Score,
                Status = snapshot.Status,
                CreatedAt = snapshot.CreatedAt
            };
        }
    }

    public class
        PropertyMatchSalesAutomationOperationsHealthSnapshotResultDto
    {
        public bool Created { get; set; }

        public Guid SnapshotId { get; set; }

        public decimal Score { get; set; }

        public string Status { get; set; }
            = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}
