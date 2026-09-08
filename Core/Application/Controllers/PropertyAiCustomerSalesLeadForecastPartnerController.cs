using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastPartnerController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastPartnerService _partnerService;

    public PropertyAiCustomerSalesLeadForecastPartnerController(
        IPropertyAiCustomerSalesLeadForecastPartnerService partnerService)
    {
        _partnerService = partnerService;
    }


    [HttpGet("dashboard/{userId}")]
    public async Task<IActionResult> GetPartnerDashboard(int userId)
    {
        var result = await _partnerService.GetDashboardAsync(userId);

        return Ok(result);
    }


    [HttpGet("forecast/{userId}")]
    public async Task<IActionResult> GetPartnerForecast(int userId)
    {
        var result = await _partnerService.GetForecastAsync(userId);

        return Ok(result);
    }
}
