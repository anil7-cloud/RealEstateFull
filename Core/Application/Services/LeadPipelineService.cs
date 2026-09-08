using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadPipelineService
{
    private readonly AppDbContext _context;

    public LeadPipelineService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<List<LeadPipeline>> GetAllAsync()
    {
        return await _context.LeadPipelines
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<List<LeadPipeline>> GetByLeadAsync(int leadId)
    {
        return await _context.LeadPipelines
            .Where(x => x.LeadId == leadId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<LeadPipeline?> GetByIdAsync(int id)
    {
        return await _context.LeadPipelines
            .FirstOrDefaultAsync(x => x.Id == id);
    }


    public async Task<LeadPipeline> CreateAsync(LeadPipeline pipeline)
    {
        pipeline.CreatedAt = DateTime.UtcNow;

        _context.LeadPipelines.Add(pipeline);

        await _context.SaveChangesAsync();

        return pipeline;
    }


    public async Task<bool> UpdateStageAsync(
        int id,
        string stage,
        int probability)
    {
        var pipeline = await _context.LeadPipelines
            .FirstOrDefaultAsync(x => x.Id == id);

        if (pipeline == null)
            return false;


        pipeline.Stage = stage;
        pipeline.Probability = probability;
        pipeline.UpdatedAt = DateTime.UtcNow;


        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> DeleteAsync(int id)
    {
        var pipeline = await _context.LeadPipelines
            .FirstOrDefaultAsync(x => x.Id == id);

        if (pipeline == null)
            return false;


        _context.LeadPipelines.Remove(pipeline);

        await _context.SaveChangesAsync();

        return true;
    }
}
