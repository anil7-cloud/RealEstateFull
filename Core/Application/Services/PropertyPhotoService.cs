using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyPhotoService
{
    private readonly AppDbContext _context;

    public PropertyPhotoService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<PropertyPhoto> AddPhoto(
        Guid propertyId,
        string imageUrl,
        string caption)
    {
        if (string.IsNullOrWhiteSpace(imageUrl))
            throw new ArgumentException("Fotoğraf bağlantısı boş olamaz.");


        var photoCount = await _context.PropertyPhotos
            .CountAsync(x => x.PropertyListingId == propertyId);


        var photo = new PropertyPhoto
        {
            PropertyListingId = propertyId,
            ImageUrl = imageUrl.Trim(),
            Caption = caption.Trim(),
            DisplayOrder = photoCount + 1,
            IsCoverPhoto = photoCount == 0,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };


        _context.PropertyPhotos.Add(photo);

        await _context.SaveChangesAsync();

        return photo;
    }


    public async Task<List<PropertyPhoto>> GetPropertyPhotos(
        Guid propertyId)
    {
        return await _context.PropertyPhotos
            .Where(x =>
                x.PropertyListingId == propertyId &&
                x.IsActive)
            .OrderByDescending(x => x.IsCoverPhoto)
            .ThenBy(x => x.DisplayOrder)
            .ToListAsync();
    }


    public async Task<PropertyPhoto?> GetCoverPhoto(
        Guid propertyId)
    {
        return await _context.PropertyPhotos
            .FirstOrDefaultAsync(x =>
                x.PropertyListingId == propertyId &&
                x.IsCoverPhoto &&
                x.IsActive);
    }


    public async Task<bool> DeletePhoto(Guid photoId)
    {
        var photo = await _context.PropertyPhotos
            .FirstOrDefaultAsync(x => x.Id == photoId);

        if (photo is null)
            return false;


        photo.IsActive = false;
        photo.IsCoverPhoto = false;


        await _context.SaveChangesAsync();

        return true;
    }
}
