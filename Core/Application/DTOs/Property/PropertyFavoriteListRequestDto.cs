namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyFavoriteListRequestDto
{
    public int UserId { get; set; }

    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 20;

    public string? Keyword { get; set; }

    public string? City { get; set; }
}
