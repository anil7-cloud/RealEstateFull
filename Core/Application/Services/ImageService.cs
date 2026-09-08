using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class ImageService
{
    private readonly AppDbContext _context;

    public ImageService(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddImage(Guid propertyId, string base64)
    {
        var image = new PropertyImage
        {
            PropertyListingId = propertyId,
            Url = $"data:image/jpeg;base64,{base64}"
        };

        _context.PropertyImages.Add(image);
        await _context.SaveChangesAsync();
    }
}
