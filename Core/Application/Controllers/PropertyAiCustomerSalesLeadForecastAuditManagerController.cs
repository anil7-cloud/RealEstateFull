using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastAuditManagerController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastCoreService _auditService;

    public PropertyAiCustomerSalesLeadForecastAuditManagerController(
        IPropertyAiCustomerSalesLeadForecastCoreService auditService)
    {
        _auditService = auditService;
    }

    [HttpGet("audit/{userId}")]
    public async Task<IActionResult> GetAudit(int userId)
    {
        var result = await _auditService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("audit/dashboard/{userId}")]
    public async Task<IActionResult> GetAuditDashboard(int userId)
    {
        var result = await _auditService.GetDashboardAsync(userId);

        return Ok(result);
    }
}
