namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadSearchDto
{
    public string? Keyword { get; set; }

    public string? Status { get; set; }

    public string? Source { get; set; }

    public int? AssignedUserId { get; set; }

    public int? PropertyId { get; set; }

    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 20;
}
