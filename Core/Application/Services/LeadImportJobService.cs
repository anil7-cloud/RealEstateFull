using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadImportJobService
{
    private readonly AppDbContext _context;

    public LeadImportJobService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<LeadImportJob> CreateAsync(LeadImportJob job)
    {
        job.CreatedAt = DateTime.UtcNow;
        job.Status = "Pending";

        _context.LeadImportJobs.Add(job);

        await _context.SaveChangesAsync();

        return job;
    }


    public async Task<List<LeadImportJob>> GetAllAsync()
    {
        return await _context.LeadImportJobs
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<LeadImportJob?> GetByIdAsync(int id)
    {
        return await _context.LeadImportJobs
            .FirstOrDefaultAsync(x => x.Id == id);
    }


    public async Task<List<LeadImportJob>> GetPendingAsync()
    {
        return await _context.LeadImportJobs
            .Where(x => x.Status == "Pending")
            .ToListAsync();
    }


    public async Task<List<LeadImportJob>> GetByUserAsync(int userId)
    {
        return await _context.LeadImportJobs
            .Where(x => x.RequestedByUserId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<bool> UpdateProgressAsync(
        int id,
        int processedRows,
        int successfulRows,
        int failedRows)
    {
        var job = await _context.LeadImportJobs
            .FirstOrDefaultAsync(x => x.Id == id);

        if(job == null)
            return false;


        job.ProcessedRows = processedRows;
        job.SuccessfulRows = successfulRows;
        job.FailedRows = failedRows;
        job.ProgressPercentage =
            job.TotalRows == 0 
            ? 0 
            : (processedRows * 100 / job.TotalRows);

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
        var job = await _context.LeadImportJobs
            .FirstOrDefaultAsync(x => x.Id == id);

        if(job == null)
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
        var job = await _context.LeadImportJobs
            .FirstOrDefaultAsync(x => x.Id == id);

        if(job == null)
            return false;


        job.Status = "Failed";
        job.ErrorMessage = error;
        job.UpdatedAt = DateTime.UtcNow;


        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> DeleteAsync(int id)
    {
        var job = await _context.LeadImportJobs
            .FirstOrDefaultAsync(x => x.Id == id);

        if(job == null)
            return false;


        job.IsActive = false;
        job.UpdatedAt = DateTime.UtcNow;


        await _context.SaveChangesAsync();

        return true;
    }
}
