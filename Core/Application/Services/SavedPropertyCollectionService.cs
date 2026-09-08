using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class SavedPropertyCollectionService
{
    private readonly AppDbContext _context;

    public SavedPropertyCollectionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<SavedPropertyCollection> CreateCollection(
        int userId,
        string name,
        string description)
    {
        var collection = new SavedPropertyCollection
        {
            UserId = userId,
            Name = name,
            Description = description,
            CreatedAt = DateTime.UtcNow
        };

        _context.SavedPropertyCollections.Add(collection);

        await _context.SaveChangesAsync();

        return collection;
    }


    public async Task<SavedPropertyCollection?> GetCollectionById(int id)
    {
        return await _context.SavedPropertyCollections
            .FirstOrDefaultAsync(x => x.Id == id);
    }


    public async Task<bool> UpdateCollection(
        int id,
        string name,
        string description,
        bool isPublic)
    {
        var collection = await _context.SavedPropertyCollections
            .FirstOrDefaultAsync(x => x.Id == id);

        if (collection is null)
            return false;

        collection.Name = name.Trim();
        collection.Description = description.Trim();
        collection.IsPublic = isPublic;
        collection.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> DeleteCollection(int id)
    {
        var collection = await _context.SavedPropertyCollections
            .FirstOrDefaultAsync(x => x.Id == id);

        if (collection is null)
            return false;

        _context.SavedPropertyCollections.Remove(collection);

        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<List<SavedPropertyCollection>> GetPublicCollections()
    {
        return await _context.SavedPropertyCollections
            .Where(x => x.IsPublic)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<int> GetCollectionCount(int userId)
    {
        return await _context.SavedPropertyCollections
            .CountAsync(x => x.UserId == userId);
    }



    public async Task<List<SavedPropertyCollection>> GetLatestCollections(int count)

    {

        if (count <= 0)

            count = 10;

        return await _context.SavedPropertyCollections

            .OrderByDescending(x => x.CreatedAt)

            .Take(count)

            .ToListAsync();

    }


    public async Task<List<SavedPropertyCollection>> SearchCollections(string keyword)
    {
        keyword = keyword.Trim();

        return await _context.SavedPropertyCollections
            .Where(x =>
                x.Name.Contains(keyword) ||
                x.Description.Contains(keyword))
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<List<SavedPropertyCollection>> GetCollectionsCreatedAfter(
        DateTime date)
    {
        return await _context.SavedPropertyCollections
            .Where(x => x.CreatedAt >= date)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<List<SavedPropertyCollection>> GetCollectionsUpdatedAfter(
        DateTime date)
    {
        return await _context.SavedPropertyCollections
            .Where(x =>
                x.UpdatedAt.HasValue &&
                x.UpdatedAt.Value >= date)
            .OrderByDescending(x => x.UpdatedAt)
            .ToListAsync();
    }

}
