using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadPushTemplateCategoryService
{
    private readonly AppDbContext _context;

    public LeadPushTemplateCategoryService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<LeadPushTemplateCategory> CreateAsync(
        string name,
        string description)
    {
        var category = new LeadPushTemplateCategory
        {
            Name = name,
            Description = description,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.LeadPushTemplateCategories.Add(category);

        await _context.SaveChangesAsync();

        return category;
    }


    public async Task<List<LeadPushTemplateCategory>> GetActiveAsync()
    {
        return await _context.LeadPushTemplateCategories
            .Where(x => x.IsActive)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }


    public async Task<bool> DisableAsync(int id)
    {
        var category = await _context.LeadPushTemplateCategories
            .FirstOrDefaultAsync(x => x.Id == id);

        if (category == null)
            return false;

        category.IsActive = false;
        category.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
