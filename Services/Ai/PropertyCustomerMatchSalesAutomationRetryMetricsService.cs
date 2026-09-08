using System.Threading;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationRetryMetricsService
    {
        private long _attempted;
        private long _succeeded;
        private long _failed;
        private long _cancelled;
        private long _claimSkipped;
        private long _backoffRejected;
        private long _leaseLost;

        private long _lastAttemptTicks;
        private long _lastSuccessTicks;
        private long _lastFailureTicks;

        public void RecordAttempt()
        {
            Interlocked.Increment(
                ref _attempted);

            Interlocked.Exchange(
                ref _lastAttemptTicks,
                DateTime.UtcNow.Ticks);
        }

        public void RecordSuccess()
        {
            Interlocked.Increment(
                ref _succeeded);

            Interlocked.Exchange(
                ref _lastSuccessTicks,
                DateTime.UtcNow.Ticks);
        }

        public void RecordFailure()
        {
            Interlocked.Increment(
                ref _failed);

            Interlocked.Exchange(
                ref _lastFailureTicks,
                DateTime.UtcNow.Ticks);
        }

        public void RecordCancelled()
        {
            Interlocked.Increment(
                ref _cancelled);
        }

        public void RecordClaimSkipped()
        {
            Interlocked.Increment(
                ref _claimSkipped);
        }

        public void RecordBackoffRejected()
        {
            Interlocked.Increment(
                ref _backoffRejected);
        }

        public void RecordLeaseLost()
        {
            Interlocked.Increment(
                ref _leaseLost);
        }

        public PropertyMatchSalesAutomationRetryMetricsDto
            GetSnapshot()
        {
            var attempted =
                Interlocked.Read(
                    ref _attempted);

            var succeeded =
                Interlocked.Read(
                    ref _succeeded);

            var failed =
                Interlocked.Read(
                    ref _failed);

            return new()
            {
                Attempted = attempted,
                Succeeded = succeeded,
                Failed = failed,

                Cancelled =
                    Interlocked.Read(
                        ref _cancelled),

                ClaimSkipped =
                    Interlocked.Read(
                        ref _claimSkipped),

                BackoffRejected =
                    Interlocked.Read(
                        ref _backoffRejected),

                LeaseLost =
                    Interlocked.Read(
                        ref _leaseLost),

                SuccessRate =
                    attempted == 0
                        ? 0
                        : Math.Round(
                            (decimal)succeeded /
                            attempted * 100m,
                            2),

                FailureRate =
                    attempted == 0
                        ? 0
                        : Math.Round(
                            (decimal)failed /
                            attempted * 100m,
                            2),

                LastAttemptAt =
                    FromTicks(
                        Interlocked.Read(
                            ref _lastAttemptTicks)),

                LastSuccessAt =
                    FromTicks(
                        Interlocked.Read(
                            ref _lastSuccessTicks)),

                LastFailureAt =
                    FromTicks(
                        Interlocked.Read(
                            ref _lastFailureTicks)),

                GeneratedAt =
                    DateTime.UtcNow
            };
        }

        private static DateTime?
            FromTicks(
                long ticks)
        {
            if (ticks <= 0)
                return null;

            return new DateTime(
                ticks,
                DateTimeKind.Utc);
        }
    }

    public class PropertyMatchSalesAutomationRetryMetricsDto
    {
        public long Attempted { get; set; }

        public long Succeeded { get; set; }

        public long Failed { get; set; }

        public long Cancelled { get; set; }

        public long ClaimSkipped { get; set; }

        public long BackoffRejected { get; set; }

        public long LeaseLost { get; set; }

        public decimal SuccessRate { get; set; }

        public decimal FailureRate { get; set; }

        public DateTime? LastAttemptAt { get; set; }

        public DateTime? LastSuccessAt { get; set; }

        public DateTime? LastFailureAt { get; set; }

        public DateTime GeneratedAt { get; set; }
    }
}
