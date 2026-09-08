using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastUserHistoryController
    : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _userService;

    public PropertyAiCustomerSalesLeadForecastUserHistoryController(
        IPropertyAiCustomerSalesLeadForecastService userService)
    {
        _userService = userService;
    }

    [HttpGet("user-history/{userId:int}")]
    public async Task<IActionResult> GetUserHistory(int userId)
    {
        if (userId <= 0)
        {
            return BadRequest(new
            {
                message = "UserId geçerli olmalıdır."
            });
        }

        var result =
            await _userService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }

    [HttpGet("user/{userId:int}")]
    public async Task<IActionResult> GetUserForecast(int userId)
    {
        if (userId <= 0)
        {
            return BadRequest(new
            {
                message = "UserId geçerli olmalıdır."
            });
        }

        var result =
            await _userService.GetForecastAsync(userId);

        return Ok(result);
    }
}
