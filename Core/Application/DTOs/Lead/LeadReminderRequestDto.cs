namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadReminderRequestDto
{
    public int LeadId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime ReminderDate { get; set; }

    public int? UserId { get; set; }
}
