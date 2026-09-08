namespace REAL_ESTATE_CLEAN.Core.Persistence
{
    public class PropertyCustomerMatchHistory
    {
        public int Id { get; set; }

        public int PropertyCustomerMatchId { get; set; }

        public int PropertyId { get; set; }

        public int LeadId { get; set; }

        public decimal? PreviousScore { get; set; }

        public decimal NewScore { get; set; }

        public string PreviousStatus { get; set; } = string.Empty;

        public string NewStatus { get; set; } = string.Empty;

        public string ChangeType { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string ChangedBy { get; set; } = "System";

        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
    }
}
