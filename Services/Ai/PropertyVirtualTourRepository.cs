using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Domain.Entities;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyVirtualTourRepository
    {
        private readonly AppDbContext _dbContext;

        public PropertyVirtualTourRepository(
            AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PropertyVirtualTour>
            CreateAsync(
                PropertyVirtualTour tour,
                CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(tour);

            if (tour.Id == Guid.Empty)
                tour.Id = Guid.NewGuid();

            tour.CreatedAt = DateTime.UtcNow;

            await _dbContext
                .PropertyVirtualTours
                .AddAsync(
                    tour,
                    cancellationToken);

            await _dbContext
                .SaveChangesAsync(
                    cancellationToken);

            return tour;
        }

        public async Task<PropertyVirtualTour?>
            GetByIdAsync(
                Guid id,
                CancellationToken cancellationToken = default)
        {
            return await _dbContext
                .PropertyVirtualTours
                .AsNoTracking()
                .Include(x => x.Scenes)
                    .ThenInclude(x => x.Hotspots)
                .Include(x => x.Models)
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task<List<PropertyVirtualTour>>
            GetByPropertyIdAsync(
                int propertyId,
                CancellationToken cancellationToken = default)
        {
            return await _dbContext
                .PropertyVirtualTours
                .AsNoTracking()
                .Where(x =>
                    x.PropertyId == propertyId &&
                    x.IsActive)
                .Include(x => x.Scenes)
                    .ThenInclude(x => x.Hotspots)
                .Include(x => x.Models)
                .OrderBy(x => x.DisplayOrder)
                .ThenBy(x => x.CreatedAt)
                .ToListAsync(
                    cancellationToken);
        }

        public async Task<PropertyVirtualTourScene>
            AddSceneAsync(
                PropertyVirtualTourScene scene,
                CancellationToken cancellationToken = default)
        {
            if (scene.Id == Guid.Empty)
                scene.Id = Guid.NewGuid();

            await _dbContext
                .PropertyVirtualTourScenes
                .AddAsync(
                    scene,
                    cancellationToken);

            await _dbContext
                .SaveChangesAsync(
                    cancellationToken);

            return scene;
        }

        public async Task<PropertyVirtualTourHotspot>
            AddHotspotAsync(
                PropertyVirtualTourHotspot hotspot,
                CancellationToken cancellationToken = default)
        {
            if (hotspot.Id == Guid.Empty)
                hotspot.Id = Guid.NewGuid();

            await _dbContext
                .PropertyVirtualTourHotspots
                .AddAsync(
                    hotspot,
                    cancellationToken);

            await _dbContext
                .SaveChangesAsync(
                    cancellationToken);

            return hotspot;
        }

        public async Task<bool>
            DeleteHotspotAsync(
                Guid hotspotId,
                CancellationToken cancellationToken = default)
        {
            var hotspot =
                await _dbContext
                    .PropertyVirtualTourHotspots
                    .FindAsync(
                        new object[] { hotspotId },
                        cancellationToken);

            if (hotspot == null)
            {
                return false;
            }

            _dbContext
                .PropertyVirtualTourHotspots
                .Remove(hotspot);

            await _dbContext
                .SaveChangesAsync(
                    cancellationToken);

            return true;
        }

        public async Task<PropertyVirtualTourHotspot?>
            UpdateHotspotAsync(
                PropertyVirtualTourHotspot hotspot,
                CancellationToken cancellationToken = default)
        {
            var existing =
                await _dbContext
                    .PropertyVirtualTourHotspots
                    .FirstOrDefaultAsync(
                        x => x.Id == hotspot.Id,
                        cancellationToken);

            if (existing == null)
                return null;

            existing.Title =
                hotspot.Title;

            existing.HotspotType =
                hotspot.HotspotType;

            existing.TargetSceneId =
                hotspot.TargetSceneId;

            existing.Yaw =
                hotspot.Yaw;

            existing.Pitch =
                hotspot.Pitch;

            existing.Description =
                hotspot.Description;

            await _dbContext
                .SaveChangesAsync(
                    cancellationToken);

            return existing;
        }



        public async Task<PropertyThreeDimensionalModel>
            AddModelAsync(
                PropertyThreeDimensionalModel model,
                CancellationToken cancellationToken = default)
        {
            if (model.Id == Guid.Empty)
                model.Id = Guid.NewGuid();

            await _dbContext
                .PropertyThreeDimensionalModels
                .AddAsync(
                    model,
                    cancellationToken);

            await _dbContext
                .SaveChangesAsync(
                    cancellationToken);

            return model;
        }

        public async Task<bool>
            DeleteAsync(
                Guid id,
                CancellationToken cancellationToken = default)
        {
            var tour =
                await _dbContext
                    .PropertyVirtualTours
                    .FirstOrDefaultAsync(
                        x => x.Id == id,
                        cancellationToken);

            if (tour == null)
                return false;

            tour.IsActive = false;
            tour.UpdatedAt = DateTime.UtcNow;

            await _dbContext
                .SaveChangesAsync(
                    cancellationToken);

            return true;
        }
    }
}
