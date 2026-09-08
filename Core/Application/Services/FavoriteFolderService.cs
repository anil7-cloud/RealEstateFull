using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class FavoriteFolderService
{
    private readonly AppDbContext _context;

    public FavoriteFolderService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<FavoriteFolder> CreateFolder(
        int userId,
        string folderName)
    {
        var folder = new FavoriteFolder
        {
            UserId = userId,
            Name = folderName,
            CreatedAt = DateTime.UtcNow
        };

        _context.FavoriteFolders.Add(folder);

        await _context.SaveChangesAsync();

        return folder;
    }
}
