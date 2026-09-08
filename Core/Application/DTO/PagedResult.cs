namespace REAL_ESTATE_CLEAN.Core.Application.DTO;

public class PagedResult<T>
{
    public List<T> Items { get; set; } = new();
    public int TotalCount { get; set; }
}
