namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertySortRequestDto
{
    public string SortBy { get; set; } = "CreatedAt";

    public string SortDirection { get; set; } = "DESC";

    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 20;
}
