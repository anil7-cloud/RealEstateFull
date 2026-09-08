using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastTrackingHistoryController
    : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _forecastService;

    public PropertyAiCustomerSalesLeadForecastTrackingHistoryController(
        IPropertyAiCustomerSalesLeadForecastService forecastService)
    {
        _forecastService = forecastService;
    }

    [HttpGet("{userId:int}")]
    public async Task<IActionResult> GetHistory(int userId)
    {
        if (userId <= 0)
        {
            return BadRequest(new
            {
                message = "UserId geçerli olmalıdır."
            });
        }

        var result =
            await _forecastService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
