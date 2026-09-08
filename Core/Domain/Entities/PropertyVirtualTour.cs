using System.Text.Json.Serialization;

namespace REAL_ESTATE_CLEAN.Core.Domain.Entities
{
    public class PropertyVirtualTour
    {
        public Guid Id { get; set; }

        public int PropertyId { get; set; }

        public string Title { get; set; }
            = string.Empty;

        /*
         * Panorama360
         * Model3D
         * VirtualTour
         */
        public string TourType { get; set; }
            = "Panorama360";

        public string? ThumbnailUrl { get; set; }

        public bool IsActive { get; set; }
            = true;

        public bool IsPublished { get; set; }

        public int DisplayOrder { get; set; }

        public DateTime CreatedAt { get; set; }
            = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public List<PropertyVirtualTourScene>
            Scenes { get; set; } = new();

        public List<PropertyThreeDimensionalModel>
            Models { get; set; } = new();
    }

    public class PropertyVirtualTourScene
    {
        public Guid Id { get; set; }

        public Guid VirtualTourId { get; set; }

        public string Name { get; set; }
            = string.Empty;

        /*
         * Equirectangular 360 derece panorama resmi.
         */
        public string PanoramaUrl { get; set; }
            = string.Empty;

        public string? ThumbnailUrl { get; set; }

        public decimal InitialYaw { get; set; }

        public decimal InitialPitch { get; set; }

        public decimal InitialFov { get; set; }
            = 75m;

        public int DisplayOrder { get; set; }

        public bool IsStartScene { get; set; }

        [JsonIgnore]
        public PropertyVirtualTour?
            VirtualTour { get; set; }

        public List<PropertyVirtualTourHotspot>
            Hotspots { get; set; } = new();
    }

    public class PropertyVirtualTourHotspot
    {
        public Guid Id { get; set; }

        public Guid SceneId { get; set; }

        public string Title { get; set; }
            = string.Empty;

        /*
         * Scene = başka odaya geçiş
         * Info  = bilgi noktası
         */
        public string HotspotType { get; set; }
            = "Scene";

        public Guid? TargetSceneId { get; set; }

        public decimal Yaw { get; set; }

        public decimal Pitch { get; set; }

        public string? Description { get; set; }

        [JsonIgnore]
        public PropertyVirtualTourScene?
            Scene { get; set; }
    }

    public class PropertyThreeDimensionalModel
    {
        public Guid Id { get; set; }

        public Guid VirtualTourId { get; set; }

        public string Name { get; set; }
            = string.Empty;

        /*
         * GLB / GLTF model.
         */
        public string ModelUrl { get; set; }
            = string.Empty;

        public string Format { get; set; }
            = "GLB";

        public string? PosterUrl { get; set; }

        public bool AllowRotation { get; set; }
            = true;

        public bool AllowZoom { get; set; }
            = true;

        public bool AutoRotate { get; set; }

        public decimal AutoRotateSpeed { get; set; }
            = 1m;

        [JsonIgnore]
        public PropertyVirtualTour?
            VirtualTour { get; set; }
    }
}
