using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastWorkflowActionController
    : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastWorkflowService _workflowService;

    public PropertyAiCustomerSalesLeadForecastWorkflowActionController(
        IPropertyAiCustomerSalesLeadForecastWorkflowService workflowService)
    {
        _workflowService = workflowService;
    }

    [HttpGet("workflow-action/{userId:int}")]
    public async Task<IActionResult> GetWorkflowAction(int userId)
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

    [HttpGet("workflow-action/history/{userId:int}")]
    public async Task<IActionResult> GetWorkflowActionHistory(int userId)
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
}
