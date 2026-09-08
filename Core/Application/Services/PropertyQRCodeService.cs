namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyQRCodeService
{
    public string GeneratePropertyUrl(
        int propertyId,
        string slug,
        string baseUrl)
    {
        baseUrl = baseUrl.TrimEnd('/');

        return $"{baseUrl}/property/{propertyId}/{slug}";
    }

    public string GenerateQrImageUrl(
        int propertyId,
        string slug,
        string baseUrl)
    {
        var propertyUrl =
            GeneratePropertyUrl(
                propertyId,
                slug,
                baseUrl);

        return $"https://api.qrserver.com/v1/create-qr-code/?size=400x400&data={Uri.EscapeDataString(propertyUrl)}";
    }

    public string GenerateShareText(
        string title,
        int propertyId,
        string slug,
        string baseUrl)
    {
        return
$"""
{title}

İlanı görüntüle:
{GeneratePropertyUrl(propertyId, slug, baseUrl)}
""";
    }
}
