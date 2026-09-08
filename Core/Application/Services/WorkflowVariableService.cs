using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class WorkflowVariableService
{
    private readonly AppDbContext _context;

    public WorkflowVariableService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<WorkflowExecutionVariable> CreateAsync(
        Guid historyId,
        string key,
        string value)
    {
        var variable = new WorkflowExecutionVariable
        {
            WorkflowExecutionHistoryId = historyId,
            Key = key,
            Value = value,
            DataType = "string",
            IsSecret = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.WorkflowExecutionVariables.Add(variable);

        await _context.SaveChangesAsync();

        return variable;
    }


    public async Task<List<WorkflowExecutionVariable>> GetByHistoryAsync(
        Guid historyId)
    {
        return await _context.WorkflowExecutionVariables
            .Where(x => x.WorkflowExecutionHistoryId == historyId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<bool> DeleteAsync(Guid id)
    {
        var variable = await _context.WorkflowExecutionVariables
            .FirstOrDefaultAsync(x => x.Id == id);

        if (variable == null)
            return false;

        _context.WorkflowExecutionVariables.Remove(variable);

        await _context.SaveChangesAsync();

        return true;
    }
}
