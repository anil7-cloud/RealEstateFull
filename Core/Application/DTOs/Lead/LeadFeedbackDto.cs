namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadFeedbackDto
{
    public int LeadId { get; set; }

    public int Rating { get; set; }

    public string Comment { get; set; } = string.Empty;

    public string FeedbackType { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
