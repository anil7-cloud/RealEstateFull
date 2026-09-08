using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadAutomationRuleService
{
    private readonly AppDbContext _context;

    public LeadAutomationRuleService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<List<LeadAutomationRule>> GetAllAsync()
    {
        return await _context.LeadAutomationRules
            .Where(x => x.IsActive)
            .OrderByDescending(x => x.Priority)
            .ToListAsync();
    }


    public async Task<LeadAutomationRule?> GetByIdAsync(int id)
    {
        return await _context.LeadAutomationRules
            .FirstOrDefaultAsync(x => x.Id == id);
    }


    public async Task<LeadAutomationRule> CreateAsync(LeadAutomationRule rule)
    {
        rule.CreatedAt = DateTime.UtcNow;
        rule.IsActive = true;

        _context.LeadAutomationRules.Add(rule);

        await _context.SaveChangesAsync();

        return rule;
    }


    public async Task<bool> UpdateAsync(LeadAutomationRule updated)
    {
        var rule = await _context.LeadAutomationRules
            .FirstOrDefaultAsync(x => x.Id == updated.Id);

        if (rule == null)
            return false;


        rule.RuleName = updated.RuleName;
        rule.TriggerEvent = updated.TriggerEvent;
        rule.ConditionExpression = updated.ConditionExpression;
        rule.ActionType = updated.ActionType;
        rule.ActionValue = updated.ActionValue;
        rule.Priority = updated.Priority;
        rule.IsEnabled = updated.IsEnabled;
        rule.RunOnlyOnce = updated.RunOnlyOnce;
        rule.Notes = updated.Notes;
        rule.UpdatedAt = DateTime.UtcNow;


        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> DeleteAsync(int id)
    {
        var rule = await _context.LeadAutomationRules
            .FirstOrDefaultAsync(x => x.Id == id);

        if (rule == null)
            return false;


        rule.IsActive = false;
        rule.UpdatedAt = DateTime.UtcNow;


        await _context.SaveChangesAsync();

        return true;
    }
}
