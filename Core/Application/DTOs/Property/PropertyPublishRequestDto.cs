namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyPublishRequestDto
{
    public int PropertyId { get; set; }

    public bool IsPublished { get; set; }

    public string PublishStatus { get; set; } = string.Empty;

    public DateTime? PublishDate { get; set; }

    public int PublishedByUserId { get; set; }

    public string Notes { get; set; } = string.Empty;
}
