using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastUserController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastCoreService _userService;

    public PropertyAiCustomerSalesLeadForecastUserController(
        IPropertyAiCustomerSalesLeadForecastCoreService userService)
    {
        _userService = userService;
    }

    [HttpGet("user/dashboard/{userId:int}")]
    public async Task<IActionResult> GetUserDashboard(int userId)
    {
        if (userId <= 0)
        {
            return BadRequest(new
            {
                message = "UserId geçerli olmalıdır."
            });
        }

        var result = await _userService.GetDashboardAsync(userId);

        return Ok(result);
    }

    [HttpGet("user/forecast/{userId:int}")]
    public async Task<IActionResult> GetUserForecast(int userId)
    {
        if (userId <= 0)
        {
            return BadRequest(new
            {
                message = "UserId geçerli olmalıdır."
            });
        }

        var result = await _userService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("user/metrics/{userId:int}")]
    public async Task<IActionResult> GetUserMetrics(int userId)
    {
        if (userId <= 0)
        {
            return BadRequest(new
            {
                message = "UserId geçerli olmalıdır."
            });
        }

        var result = await _userService.GetMetricsAsync(userId);

        return Ok(result);
    }
}
