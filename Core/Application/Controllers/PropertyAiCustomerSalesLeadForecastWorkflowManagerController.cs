using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastWorkflowManagerController
    : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastWorkflowService _workflowService;

    public PropertyAiCustomerSalesLeadForecastWorkflowManagerController(
        IPropertyAiCustomerSalesLeadForecastWorkflowService workflowService)
    {
        _workflowService = workflowService;
    }

    [HttpGet("workflow/{userId:int}")]
    public async Task<IActionResult> GetWorkflow(int userId)
    {
        if (userId <= 0)
        {
            return BadRequest(new
            {
                message = "UserId geçerli olmalıdır."
            });
        }

        var result =
            await _workflowService.GetWorkflowAsync(userId);

        return Ok(result);
    }

    [HttpGet("workflow/history/{userId:int}")]
    public async Task<IActionResult> GetWorkflowHistory(int userId)
    {
        if (userId <= 0)
        {
            return BadRequest(new
            {
                message = "UserId geçerli olmalıdır."
            });
        }

        var result =
            await _workflowService.GetHistoryAsync(userId);

        return Ok(result);
    }

    [HttpGet("workflow/forecast/{userId:int}")]
    public async Task<IActionResult> GetWorkflowForecast(int userId)
    {
        if (userId <= 0)
        {
            return BadRequest(new
            {
                message = "UserId geçerli olmalıdır."
            });
        }

        var result =
            await _workflowService.GetWorkflowAsync(userId);

        return Ok(result);
    }
}
