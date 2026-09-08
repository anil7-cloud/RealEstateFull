using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastSearchController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _searchService;

    public PropertyAiCustomerSalesLeadForecastSearchController(
        IPropertyAiCustomerSalesLeadForecastService searchService)
    {
        _searchService = searchService;
    }

    [HttpGet("search/{userId}")]
    public async Task<IActionResult> Search(int userId)
    {
        var result = await _searchService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("search-analysis/{userId}")]
    public async Task<IActionResult> SearchAnalysis(int userId)
    {
        var result = await _searchService.AnalyzeAsync(userId);

        return Ok(result);
    }
}
