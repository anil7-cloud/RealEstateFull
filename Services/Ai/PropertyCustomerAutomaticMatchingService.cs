using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerAutomaticMatchingService
    {
        private readonly AppDbContext _context;
        private readonly PropertyCustomerAutoMatchService _autoMatchService;

        public PropertyCustomerAutomaticMatchingService(
            AppDbContext context,
            PropertyCustomerAutoMatchService autoMatchService)
        {
            _context = context;
            _autoMatchService = autoMatchService;
        }

        public async Task<object> MatchAsync(
            int propertyId,
            int leadId)
        {
            if (propertyId <= 0)
                throw new ArgumentOutOfRangeException(nameof(propertyId));

            if (leadId <= 0)
                throw new ArgumentOutOfRangeException(nameof(leadId));

            var property = await _context.Properties
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == propertyId);

            if (property == null)
                throw new InvalidOperationException(
                    $"Property bulunamadı. Id: {propertyId}");

            var leadExists = await _context.Leads
                .AsNoTracking()
                .AnyAsync(x => x.Id == leadId);

            if (!leadExists)
                throw new InvalidOperationException(
                    $"Lead bulunamadı. Id: {leadId}");

            var requirements = await _context.LeadRequirements
                .AsNoTracking()
                .Where(x =>
                    x.LeadId == leadId &&
                    x.IsActive)
                .ToListAsync();

            if (requirements.Count == 0)
                throw new InvalidOperationException(
                    "Bu müşterinin aktif eşleşme kriteri bulunamadı.");

            var valuation = await _context.Set<PropertyValuation>()
                .AsNoTracking()
                .Where(x => x.PropertyId == propertyId)
                .OrderByDescending(x => x.ValuationDate)
                .FirstOrDefaultAsync();

            var maxBudget = GetDecimal(
                requirements,
                "MaxBudget");

            var city = GetString(
                requirements,
                "City");

            var district = GetString(
                requirements,
                "District");

            var propertyType = GetString(
                requirements,
                "PropertyType");

            var roomCount = GetString(
                requirements,
                "RoomCount");

            var minSize = GetDecimal(
                requirements,
                "MinSize");

            var maxSize = GetDecimal(
                requirements,
                "MaxSize");

            var priceMatched =
                !maxBudget.HasValue ||
                property.Price <= maxBudget.Value;

            var cityMatched =
                string.IsNullOrWhiteSpace(city) ||
                TextEquals(property.City, city);

            var districtMatched =
                string.IsNullOrWhiteSpace(district) ||
                TextEquals(property.District, district) ||
                TextEquals(property.Location, district);

            var locationMatched =
                cityMatched &&
                districtMatched;

            var propertyTypeMatched =
                string.IsNullOrWhiteSpace(propertyType) ||
                TextEquals(
                    property.PropertyType,
                    propertyType);

            var roomCountMatched =
                string.IsNullOrWhiteSpace(roomCount) ||
                TextEquals(
                    property.RoomCount,
                    roomCount);

            var sizeMatched =
                CalculateSizeMatched(
                    valuation?.AreaSquareMeters,
                    minSize,
                    maxSize);

            var scoreRequest =
                new PropertyMatchScoreRequest
                {
                    PriceMatched = priceMatched,
                    LocationMatched = locationMatched,
                    PropertyTypeMatched = propertyTypeMatched,
                    RoomCountMatched = roomCountMatched,
                    SizeMatched = sizeMatched
                };

            var match = await _autoMatchService.CreateMatchAsync(
                propertyId,
                leadId,
                scoreRequest);

            return new
            {
                Match = match,

                Property = new
                {
                    property.Id,
                    property.Title,
                    property.Price,
                    property.City,
                    property.District,
                    property.Location,
                    property.PropertyType,
                    property.RoomCount,
                    AreaSquareMeters =
                        valuation?.AreaSquareMeters
                },

                Criteria = new
                {
                    MaxBudget = maxBudget,
                    City = city,
                    District = district,
                    PropertyType = propertyType,
                    RoomCount = roomCount,
                    MinSize = minSize,
                    MaxSize = maxSize
                },

                Evaluation = new
                {
                    PriceMatched = priceMatched,
                    LocationMatched = locationMatched,
                    PropertyTypeMatched =
                        propertyTypeMatched,
                    RoomCountMatched =
                        roomCountMatched,
                    SizeMatched = sizeMatched
                }
            };
        }

        private static string? GetString(
            IEnumerable<LeadRequirement> requirements,
            string name)
        {
            return requirements
                .FirstOrDefault(x =>
                    string.Equals(
                        x.RequirementName,
                        name,
                        StringComparison.OrdinalIgnoreCase))
                ?.RequirementValue
                ?.Trim();
        }

        private static decimal? GetDecimal(
            IEnumerable<LeadRequirement> requirements,
            string name)
        {
            var value =
                GetString(requirements, name);

            if (string.IsNullOrWhiteSpace(value))
                return null;

            return decimal.TryParse(
                value,
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out var result)
                    ? result
                    : null;
        }

        private static bool CalculateSizeMatched(
            decimal? area,
            decimal? minSize,
            decimal? maxSize)
        {
            if (!minSize.HasValue &&
                !maxSize.HasValue)
            {
                return true;
            }

            if (!area.HasValue)
                return false;

            if (minSize.HasValue &&
                area.Value < minSize.Value)
            {
                return false;
            }

            if (maxSize.HasValue &&
                area.Value > maxSize.Value)
            {
                return false;
            }

            return true;
        }

        private static bool TextEquals(
            string? left,
            string? right)
        {
            if (string.IsNullOrWhiteSpace(left) ||
                string.IsNullOrWhiteSpace(right))
            {
                return false;
            }

            return string.Equals(
                left.Trim(),
                right.Trim(),
                StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
