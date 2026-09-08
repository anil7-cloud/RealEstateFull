using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastTrainingController
    : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastCoreService _trainingService;

    public PropertyAiCustomerSalesLeadForecastTrainingController(
        IPropertyAiCustomerSalesLeadForecastCoreService trainingService)
    {
        _trainingService = trainingService;
    }

    [HttpGet("training/{userId:int}")]
    public async Task<IActionResult> GetTraining(int userId)
    {
        if (userId <= 0)
        {
            return BadRequest(new
            {
                message = "UserId geçerli olmalıdır."
            });
        }

        var result =
            await _trainingService.GetMetricsAsync(userId);

        return Ok(result);
    }

    [HttpGet("training/dashboard/{userId:int}")]
    public async Task<IActionResult> GetTrainingDashboard(int userId)
    {
        if (userId <= 0)
        {
            return BadRequest(new
            {
                message = "UserId geçerli olmalıdır."
            });
        }

        var result =
            await _trainingService.GetDashboardAsync(userId);

        return Ok(result);
    }

    [HttpGet("training/forecast/{userId:int}")]
    public async Task<IActionResult> GetTrainingForecast(int userId)
    {
        if (userId <= 0)
        {
            return BadRequest(new
            {
                message = "UserId geçerli olmalıdır."
            });
        }

        var result =
            await _trainingService.GetForecastAsync(userId);

        return Ok(result);
    }
}
