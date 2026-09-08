namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class OpenHouse
{
    public int Id { get; set; }

    public int PropertyId { get; set; }

    public DateTime EventDate { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int MaxParticipants { get; set; } = 20;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
