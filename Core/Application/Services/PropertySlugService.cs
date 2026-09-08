using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertySlugService
{
    public string GenerateSlug(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return string.Empty;

        text = ReplaceTurkishCharacters(text.Trim().ToLowerInvariant());

        text = text.Normalize(NormalizationForm.FormD);

        var builder = new StringBuilder();

        foreach (var character in text)
        {
            var category =
                CharUnicodeInfo.GetUnicodeCategory(character);

            if (category != UnicodeCategory.NonSpacingMark)
                builder.Append(character);
        }

        text = builder
            .ToString()
            .Normalize(NormalizationForm.FormC);

        text = Regex.Replace(
            text,
            @"[^a-z0-9\s-]",
            string.Empty);

        text = Regex.Replace(
            text,
            @"[\s-]+",
            "-");

        return text.Trim('-');
    }

    public string GeneratePropertySlug(
        string title,
        int propertyId)
    {
        var slug = GenerateSlug(title);

        return string.IsNullOrWhiteSpace(slug)
            ? propertyId.ToString()
            : $"{slug}-{propertyId}";
    }

    private static string ReplaceTurkishCharacters(string text)
    {
        return text
            .Replace("ç", "c")
            .Replace("ğ", "g")
            .Replace("ı", "i")
            .Replace("ö", "o")
            .Replace("ş", "s")
            .Replace("ü", "u");
    }
}
