
namespace REAL_ESTATE_CLEAN.Infrastructure.Storage;

public class ImageStorageService
{
    public string Upload(string fileName)
    {
        return "https://cdn.realestate.com/" + fileName;
    }
}
