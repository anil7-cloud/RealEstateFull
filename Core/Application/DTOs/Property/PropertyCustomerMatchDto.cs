using System;

namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property
{
    public class PropertyCustomerMatchDto
    {
        public int Id { get; set; }

        public int PropertyId { get; set; }

        public int LeadId { get; set; }

        public decimal MatchScore { get; set; }

        public bool PriceMatched { get; set; }

        public bool LocationMatched { get; set; }

        public bool PropertyTypeMatched { get; set; }

        public bool RoomCountMatched { get; set; }

        public bool SizeMatched { get; set; }

        public string MatchReason { get; set; } = string.Empty;

        public string Status { get; set; } = "New";

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
