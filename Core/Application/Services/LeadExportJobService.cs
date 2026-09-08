using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadExportJobService
{
    private readonly AppDbContext _context;

    public LeadExportJobService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<LeadExportJob> CreateAsync(
        LeadExportJob job)
    {
        job.CreatedAt = DateTime.UtcNow;
        job.Status = "Pending";

        _context.LeadExportJobs.Add(job);

        await _context.SaveChangesAsync();

        return job;
    }


    public async Task<List<LeadExportJob>> GetAllAsync()
    {
        return await _context.LeadExportJobs
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<List<LeadExportJob>> GetByUserAsync(int userId)
    {
        return await _context.LeadExportJobs
            .Where(x => x.RequestedByUserId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<List<LeadExportJob>> GetPendingAsync()
    {
        return await _context.LeadExportJobs
            .Where(x => x.Status == "Pending")
            .OrderBy(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<LeadExportJob?> GetByIdAsync(
        int id)
    {
        return await _context.LeadExportJobs
            .FirstOrDefaultAsync(x => x.Id == id);
    }


    public async Task<bool> StartAsync(
        int id)
    {
        var job = await _context.LeadExportJobs
            .FirstOrDefaultAsync(x => x.Id == id);

        if (job == null)
            return false;


        job.Status = "Running";
        job.StartedAt = DateTime.UtcNow;
        job.UpdatedAt = DateTime.UtcNow;


        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> UpdateProgressAsync(
        int id,
        int progress)
    {
        var job = await _context.LeadExportJobs
            .FirstOrDefaultAsync(x => x.Id == id);

        if (job == null)
            return false;


        job.ProgressPercentage = progress;
        job.UpdatedAt = DateTime.UtcNow;


        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> CompleteAsync(
        int id,
        string fileName,
        string filePath,
        long fileSize)
    {
        var job = await _context.LeadExportJobs
            .FirstOrDefaultAsync(x => x.Id == id);

        if (job == null)
            return false;


        job.Status = "Completed";
        job.FileName = fileName;
        job.FilePath = filePath;
        job.FileSize = fileSize;
        job.CompletedAt = DateTime.UtcNow;
        job.UpdatedAt = DateTime.UtcNow;


        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> FailAsync(
        int id,
        string error)
    {
        var job = await _context.LeadExportJobs
            .FirstOrDefaultAsync(x => x.Id == id);

        if (job == null)
            return false;


        job.Status = "Failed";
        job.ErrorMessage = error;
        job.UpdatedAt = DateTime.UtcNow;


        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> DeleteAsync(
        int id)
    {
        var job = await _context.LeadExportJobs
            .FirstOrDefaultAsync(x => x.Id == id);

        if (job == null)
            return false;


        _context.LeadExportJobs.Remove(job);

        await _context.SaveChangesAsync();

        return true;
    }
}
