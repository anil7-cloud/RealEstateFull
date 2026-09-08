using Microsoft.AspNetCore.Http;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyVirtualTourUploadService
    {
        private readonly IWebHostEnvironment _environment;

        private const long MaxPanoramaBytes =
            30 * 1024 * 1024;

        private const long MaxModelBytes =
            150 * 1024 * 1024;

        public PropertyVirtualTourUploadService(
            IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public Task<PropertyVirtualTourUploadResultDto>
            UploadPanoramaAsync(
                int propertyId,
                IFormFile file,
                CancellationToken cancellationToken = default)
        {
            var allowedExtensions =
                new HashSet<string>(
                    StringComparer.OrdinalIgnoreCase)
                {
                    ".jpg",
                    ".jpeg",
                    ".png",
                    ".webp"
                };

            return SaveAsync(
                propertyId,
                file,
                "panoramas",
                allowedExtensions,
                MaxPanoramaBytes,
                cancellationToken);
        }

        public Task<PropertyVirtualTourUploadResultDto>
            UploadModelAsync(
                int propertyId,
                IFormFile file,
                CancellationToken cancellationToken = default)
        {
            var allowedExtensions =
                new HashSet<string>(
                    StringComparer.OrdinalIgnoreCase)
                {
                    ".glb",
                    ".gltf"
                };

            return SaveAsync(
                propertyId,
                file,
                "models",
                allowedExtensions,
                MaxModelBytes,
                cancellationToken);
        }

        private async Task<PropertyVirtualTourUploadResultDto>
            SaveAsync(
                int propertyId,
                IFormFile file,
                string category,
                HashSet<string> allowedExtensions,
                long maxBytes,
                CancellationToken cancellationToken)
        {
            if (propertyId <= 0)
            {
                throw new ArgumentException(
                    "PropertyId geçersiz.");
            }

            if (file == null ||
                file.Length == 0)
            {
                throw new ArgumentException(
                    "Dosya boş.");
            }

            if (file.Length > maxBytes)
            {
                throw new ArgumentException(
                    $"Dosya çok büyük. Maksimum {maxBytes / 1024 / 1024} MB.");
            }

            var extension =
                Path.GetExtension(
                    file.FileName);

            if (string.IsNullOrWhiteSpace(
                    extension) ||
                !allowedExtensions.Contains(
                    extension))
            {
                throw new ArgumentException(
                    "Desteklenmeyen dosya formatı.");
            }

            extension =
                extension.ToLowerInvariant();

            var webRoot =
                _environment.WebRootPath;

            if (string.IsNullOrWhiteSpace(
                    webRoot))
            {
                webRoot =
                    Path.Combine(
                        _environment.ContentRootPath,
                        "wwwroot");
            }

            var relativeDirectory =
                Path.Combine(
                    "uploads",
                    "property-tours",
                    propertyId.ToString(),
                    category);

            var physicalDirectory =
                Path.Combine(
                    webRoot,
                    relativeDirectory);

            Directory.CreateDirectory(
                physicalDirectory);

            /*
             * Kullanıcının gönderdiği dosya adını
             * doğrudan kullanmıyoruz.
             */
            var storedFileName =
                $"{Guid.NewGuid():N}{extension}";

            var physicalPath =
                Path.Combine(
                    physicalDirectory,
                    storedFileName);

            await using var stream =
                new FileStream(
                    physicalPath,
                    FileMode.CreateNew,
                    FileAccess.Write,
                    FileShare.None,
                    81920,
                    useAsync: true);

            await file.CopyToAsync(
                stream,
                cancellationToken);

            var url =
                "/" +
                Path.Combine(
                        relativeDirectory,
                        storedFileName)
                    .Replace(
                        Path.DirectorySeparatorChar,
                        '/');

            return new()
            {
                OriginalFileName =
                    Path.GetFileName(
                        file.FileName),

                StoredFileName =
                    storedFileName,

                Url =
                    url,

                Extension =
                    extension,

                ContentType =
                    file.ContentType,

                SizeBytes =
                    file.Length,

                UploadedAt =
                    DateTime.UtcNow
            };
        }
    }

    public class PropertyVirtualTourUploadResultDto
    {
        public string OriginalFileName { get; set; } =
            string.Empty;

        public string StoredFileName { get; set; } =
            string.Empty;

        public string Url { get; set; } =
            string.Empty;

        public string Extension { get; set; } =
            string.Empty;

        public string ContentType { get; set; } =
            string.Empty;

        public long SizeBytes { get; set; }

        public DateTime UploadedAt { get; set; }
    }
}
