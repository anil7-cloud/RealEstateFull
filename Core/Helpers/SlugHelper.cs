using System.Text;

namespace REAL_ESTATE_CLEAN.Core.Helpers;

public static class SlugHelper
{
    public static string Generate(string text)
    {
        return text
            .ToLower()
            .Replace(" ", "-")
            .Replace("ç", "c")
            .Replace("ğ", "g")
            .Replace("ı", "i")
            .Replace("ö", "o")
            .Replace("ş", "s")
            .Replace("ü", "u");
    }
}
