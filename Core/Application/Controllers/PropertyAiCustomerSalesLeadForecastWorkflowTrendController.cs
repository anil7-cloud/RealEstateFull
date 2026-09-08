using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastWorkflowTrendController
    : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastWorkflowService _workflowTrendService;

    public PropertyAiCustomerSalesLeadForecastWorkflowTrendController(
        IPropertyAiCustomerSalesLeadForecastWorkflowService workflowTrendService)
    {
        _workflowTrendService = workflowTrendService;
    }

    [HttpGet("workflow-trend/{userId:int}")]
    public async Task<IActionResult> GetWorkflowTrend(int userId)
    {
        if (userId <= 0)
        {
            return BadRequest(new
            {
                message = "UserId geçerli olmalıdır."
            });
        }

        var result =
            await _workflowTrendService.GetWorkflowAsync(userId);

        return Ok(result);
    }

    [HttpGet("workflow-trend/history/{userId:int}")]
    public async Task<IActionResult> GetWorkflowTrendHistory(int userId)
    {
        if (userId <= 0)
        {
            return BadRequest(new
            {
                message = "UserId geçerli olmalıdır."
            });
        }

        var result =
            await _workflowTrendService.GetHistoryAsync(userId);

        return Ok(result);
    }
}
