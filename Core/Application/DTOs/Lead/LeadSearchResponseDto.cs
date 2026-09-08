namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadSearchResponseDto
{
    public List<LeadSummaryDto> Results { get; set; } = new();

    public int TotalCount { get; set; }

    public string? Keyword { get; set; }

    public int Page { get; set; }

    public int PageSize { get; set; }

    public DateTime SearchedAt { get; set; } = DateTime.UtcNow;
}
