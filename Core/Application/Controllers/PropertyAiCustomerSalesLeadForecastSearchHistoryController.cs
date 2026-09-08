using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastSearchHistoryController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _searchService;

    public PropertyAiCustomerSalesLeadForecastSearchHistoryController(
        IPropertyAiCustomerSalesLeadForecastService searchService)
    {
        _searchService = searchService;
    }

    [HttpGet("search-history/{userId}")]
    public async Task<IActionResult> GetSearchHistory(int userId)
    {
        var result = await _searchService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }

    [HttpGet("search/{userId}")]
    public async Task<IActionResult> Search(int userId)
    {
        var result = await _searchService.GetForecastAsync(userId);

        return Ok(result);
    }
}
