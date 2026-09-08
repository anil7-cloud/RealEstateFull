namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadMeeting
{
    public int Id { get; set; }

    // Potansiyel müşteri
    public int LeadId { get; set; }

    // İlgili portföy
    public int? PropertyId { get; set; }

    // Toplantıyı gerçekleştirecek danışman
    public int? UserId { get; set; }

    // Meeting, PropertyShowing, OfficeMeeting, OnlineMeeting
    public string MeetingType { get; set; } = "Meeting";

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    // Adres
    public string Location { get; set; } = string.Empty;

    // Google Meet / Zoom vb.
    public string MeetingUrl { get; set; } = string.Empty;

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    // Scheduled, Completed, Cancelled, NoShow
    public string Status { get; set; } = "Scheduled";

    public bool ReminderSent { get; set; }

    public string Result { get; set; } = string.Empty;

    public string FollowUpNote { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
