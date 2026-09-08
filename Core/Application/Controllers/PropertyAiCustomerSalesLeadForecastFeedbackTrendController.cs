using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastFeedbackTrendController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _feedbackTrendService;

    public PropertyAiCustomerSalesLeadForecastFeedbackTrendController(
        IPropertyAiCustomerSalesLeadForecastService feedbackTrendService)
    {
        _feedbackTrendService = feedbackTrendService;
    }

    [HttpGet("feedback-trend/{userId}")]
    public async Task<IActionResult> GetFeedbackTrend(int userId)
    {
        var result = await _feedbackTrendService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("feedback-trend/history/{userId}")]
    public async Task<IActionResult> GetFeedbackTrendHistory(int userId)
    {
        var result = await _feedbackTrendService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
