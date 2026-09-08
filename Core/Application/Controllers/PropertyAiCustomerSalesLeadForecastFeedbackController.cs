using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastFeedbackController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastCoreService _feedbackService;

    public PropertyAiCustomerSalesLeadForecastFeedbackController(
        IPropertyAiCustomerSalesLeadForecastCoreService feedbackService)
    {
        _feedbackService = feedbackService;
    }

    [HttpGet("feedback/{userId}")]
    public async Task<IActionResult> GetFeedback(int userId)
    {
        var result = await _feedbackService.GetMetricsAsync(userId);

        return Ok(result);
    }

    [HttpGet("feedback/dashboard/{userId}")]
    public async Task<IActionResult> GetFeedbackDashboard(int userId)
    {
        var result = await _feedbackService.GetDashboardAsync(userId);

        return Ok(result);
    }
}
