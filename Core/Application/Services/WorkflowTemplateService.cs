using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class WorkflowTemplateService
{
    private readonly AppDbContext _context;

    public WorkflowTemplateService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<WorkflowExecutionTemplate> CreateAsync(
        string name,
        string description)
    {
        var template = new WorkflowExecutionTemplate
        {
            Name = name,
            Description = description,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.WorkflowExecutionTemplates.Add(template);

        await _context.SaveChangesAsync();

        return template;
    }


    public async Task<List<WorkflowExecutionTemplate>> GetAllAsync()
    {
        return await _context.WorkflowExecutionTemplates
            .Where(x => x.IsActive)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<WorkflowExecutionTemplate?> GetByIdAsync(
        Guid id)
    {
        return await _context.WorkflowExecutionTemplates
            .FirstOrDefaultAsync(x => x.Id == id);
    }


    public async Task<bool> DeleteAsync(Guid id)
    {
        var template = await _context.WorkflowExecutionTemplates
            .FirstOrDefaultAsync(x => x.Id == id);

        if (template == null)
            return false;

        template.IsActive = false;

        await _context.SaveChangesAsync();

        return true;
    }
}
