namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyCreateResponseDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public string Message { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}
