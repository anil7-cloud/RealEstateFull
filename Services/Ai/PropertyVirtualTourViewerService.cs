namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyVirtualTourViewerService
    {
        private readonly PropertyVirtualTourRepository
            _repository;

        public PropertyVirtualTourViewerService(
            PropertyVirtualTourRepository repository)
        {
            _repository = repository;
        }

        public async Task<PropertyVirtualTourViewerDto?>
            GetViewerAsync(
                Guid tourId,
                CancellationToken cancellationToken = default)
        {
            var tour =
                await _repository.GetByIdAsync(
                    tourId,
                    cancellationToken);

            if (tour == null ||
                !tour.IsActive)
            {
                return null;
            }

            var scenes =
                tour.Scenes
                    .OrderBy(x => x.DisplayOrder)
                    .Select(x =>
                        new PropertyVirtualTourViewerSceneDto
                        {
                            Id = x.Id,
                            Name = x.Name,
                            PanoramaUrl = x.PanoramaUrl,
                            ThumbnailUrl = x.ThumbnailUrl,
                            InitialYaw = x.InitialYaw,
                            InitialPitch = x.InitialPitch,
                            InitialFov = x.InitialFov,
                            IsStartScene = x.IsStartScene,

                            Hotspots =
                                x.Hotspots
                                    .Select(h =>
                                        new PropertyVirtualTourViewerHotspotDto
                                        {
                                            Id = h.Id,
                                            Title = h.Title,
                                            Type = h.HotspotType,
                                            TargetSceneId = h.TargetSceneId,
                                            Yaw = h.Yaw,
                                            Pitch = h.Pitch,
                                            Description = h.Description
                                        })
                                    .ToList()
                        })
                    .ToList();

            var startScene =
                scenes.FirstOrDefault(x =>
                    x.IsStartScene)
                ?? scenes.FirstOrDefault();

            return new()
            {
                TourId = tour.Id,
                PropertyId = tour.PropertyId,
                Title = tour.Title,
                StartSceneId = startScene?.Id,
                Scenes = scenes,

                Models =
                    tour.Models
                        .Select(x =>
                            new PropertyVirtualTourViewerModelDto
                            {
                                Id = x.Id,
                                Name = x.Name,
                                ModelUrl = x.ModelUrl,
                                Format = x.Format,
                                PosterUrl = x.PosterUrl,
                                AllowRotation = x.AllowRotation,
                                AllowZoom = x.AllowZoom,
                                AutoRotate = x.AutoRotate,
                                AutoRotateSpeed = x.AutoRotateSpeed
                            })
                        .ToList()
            };
        }
    }

    public class PropertyVirtualTourViewerDto
    {
        public Guid TourId { get; set; }

        public int PropertyId { get; set; }

        public string Title { get; set; } =
            string.Empty;

        public Guid? StartSceneId { get; set; }

        public List<PropertyVirtualTourViewerSceneDto>
            Scenes { get; set; } = new();

        public List<PropertyVirtualTourViewerModelDto>
            Models { get; set; } = new();
    }

    public class PropertyVirtualTourViewerSceneDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } =
            string.Empty;

        public string PanoramaUrl { get; set; } =
            string.Empty;

        public string? ThumbnailUrl { get; set; }

        public decimal InitialYaw { get; set; }

        public decimal InitialPitch { get; set; }

        public decimal InitialFov { get; set; }

        public bool IsStartScene { get; set; }

        public List<PropertyVirtualTourViewerHotspotDto>
            Hotspots { get; set; } = new();
    }

    public class PropertyVirtualTourViewerHotspotDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; } =
            string.Empty;

        public string Type { get; set; } =
            string.Empty;

        public Guid? TargetSceneId { get; set; }

        public decimal Yaw { get; set; }

        public decimal Pitch { get; set; }

        public string? Description { get; set; }
    }

    public class PropertyVirtualTourViewerModelDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } =
            string.Empty;

        public string ModelUrl { get; set; } =
            string.Empty;

        public string Format { get; set; } =
            string.Empty;

        public string? PosterUrl { get; set; }

        public bool AllowRotation { get; set; }

        public bool AllowZoom { get; set; }

        public bool AutoRotate { get; set; }

        public decimal AutoRotateSpeed { get; set; }
    }
}
