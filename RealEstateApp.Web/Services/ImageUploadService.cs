using Microsoft.AspNetCore.Components.Forms;

namespace RealEstateApp.Web.Services;

public class ImageUploadService
{
    private readonly IWebHostEnvironment _environment;

    public ImageUploadService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<string?> SaveImageAsync(IBrowserFile file)
    {
        if (file is null)
            return null;

        var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");

        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var extension = Path.GetExtension(file.Name);
        var fileName = $"{Guid.NewGuid()}{extension}";
        var filePath = Path.Combine(uploadsFolder, fileName);

        await using var inputStream = file.OpenReadStream(10 * 1024 * 1024);
        await using var outputStream = new FileStream(filePath, FileMode.Create);
        await inputStream.CopyToAsync(outputStream);

        return $"/uploads/{fileName}";
    }
}
