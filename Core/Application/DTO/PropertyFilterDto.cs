namespace REAL_ESTATE_CLEAN.Core.Application.DTO;

public class PropertyFilterDto
{
    public string? City { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
}
