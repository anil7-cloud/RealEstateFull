namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyFavoriteRequestDto
{
    public int PropertyId { get; set; }

    public int UserId { get; set; }

    public bool IsFavorite { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
