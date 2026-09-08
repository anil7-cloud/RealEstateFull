using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers;

[ApiController]
[Route("api/ai/forecast-summary")]
public class PropertyAiCustomerSalesLeadForecastSummaryController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastSummaryService _service;

    public PropertyAiCustomerSalesLeadForecastSummaryController(
        IPropertyAiCustomerSalesLeadForecastSummaryService service)
    {
        _service = service;
    }


    [HttpGet("{userId}")]
    public async Task<IActionResult> GetSummary(int userId)
    {
        return Ok(await _service.GetSummaryAsync(userId));
    }


    [HttpGet("{userId}/history")]
    public async Task<IActionResult> GetHistory(int userId)
    {
        return Ok(await _service.GetHistoryAsync(userId));
    }
}
