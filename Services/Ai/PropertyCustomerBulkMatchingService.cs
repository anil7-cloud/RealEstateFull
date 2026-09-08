using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerBulkMatchingService
    {
        private readonly AppDbContext _context;
        private readonly PropertyCustomerAutomaticMatchingService
            _automaticMatchingService;

        public PropertyCustomerBulkMatchingService(
            AppDbContext context,
            PropertyCustomerAutomaticMatchingService automaticMatchingService)
        {
            _context = context;
            _automaticMatchingService = automaticMatchingService;
        }

        public async Task<BulkLeadMatchResult> GenerateForLeadAsync(
            int leadId,
            int limit = 100)
        {
            if (leadId <= 0)
                throw new ArgumentOutOfRangeException(nameof(leadId));

            if (limit < 1)
                limit = 100;

            limit = Math.Min(limit, 500);

            var leadExists = await _context.Leads
                .AsNoTracking()
                .AnyAsync(x => x.Id == leadId);

            if (!leadExists)
                throw new InvalidOperationException(
                    $"Lead bulunamadı. Id: {leadId}");

            var hasRequirements = await _context.LeadRequirements
                .AsNoTracking()
                .AnyAsync(x =>
                    x.LeadId == leadId &&
                    x.IsActive);

            if (!hasRequirements)
                throw new InvalidOperationException(
                    "Lead için aktif kriter bulunamadı.");

            var propertyIds = await _context.Properties
                .AsNoTracking()
                .Where(x => x.IsActive)
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => x.Id)
                .Take(limit)
                .ToListAsync();

            var successfulMatchCount = 0;
            var failed = new List<BulkMatchFailure>();

            foreach (var propertyId in propertyIds)
            {
                try
                {
                    await _automaticMatchingService.MatchAsync(
                        propertyId,
                        leadId);

                    successfulMatchCount++;
                }
                catch (Exception ex)
                {
                    failed.Add(new BulkMatchFailure
                    {
                        PropertyId = propertyId,
                        Error = ex.Message
                    });
                }
            }

            var matchRows = await _context
                .Set<PropertyCustomerMatch>()
                .AsNoTracking()
                .Where(x => x.LeadId == leadId)
                .ToListAsync();

            var recommendations = matchRows
                .OrderByDescending(x => x.MatchScore)
                .ThenByDescending(x => x.UpdatedAt ?? x.CreatedAt)
                .Take(10)
                .Select(x => new BulkMatchRecommendation
                {
                    MatchId = x.Id,
                    PropertyId = x.PropertyId,
                    LeadId = x.LeadId,
                    Score = x.MatchScore,
                    Status = x.Status,
                    Reason = x.MatchReason
                })
                .ToList();

            return new BulkLeadMatchResult
            {
                LeadId = leadId,
                ProcessedPropertyCount = propertyIds.Count,
                SuccessfulMatchCount = successfulMatchCount,
                FailedMatchCount = failed.Count,
                Recommendations = recommendations,
                Failures = failed
            };
        }

        public async Task<BulkPropertyMatchResult> GenerateForPropertyAsync(
            int propertyId,
            int limit = 100)
        {
            if (propertyId <= 0)
                throw new ArgumentOutOfRangeException(nameof(propertyId));

            if (limit < 1)
                limit = 100;

            limit = Math.Min(limit, 500);

            var propertyExists = await _context.Properties
                .AsNoTracking()
                .AnyAsync(x =>
                    x.Id == propertyId &&
                    x.IsActive);

            if (!propertyExists)
                throw new InvalidOperationException(
                    $"Aktif property bulunamadı. Id: {propertyId}");

            var leadIds = await _context.Leads
                .AsNoTracking()
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => x.Id)
                .Take(limit)
                .ToListAsync();

            var successfulMatchCount = 0;
            var failed = new List<BulkPropertyLeadFailure>();

            foreach (var leadId in leadIds)
            {
                try
                {
                    var hasRequirements = await _context.LeadRequirements
                        .AsNoTracking()
                        .AnyAsync(x =>
                            x.LeadId == leadId &&
                            x.IsActive);

                    if (!hasRequirements)
                    {
                        failed.Add(new BulkPropertyLeadFailure
                        {
                            LeadId = leadId,
                            Error = "Lead için aktif kriter bulunamadı."
                        });

                        continue;
                    }

                    await _automaticMatchingService.MatchAsync(
                        propertyId,
                        leadId);

                    successfulMatchCount++;
                }
                catch (Exception ex)
                {
                    failed.Add(new BulkPropertyLeadFailure
                    {
                        LeadId = leadId,
                        Error = ex.Message
                    });
                }
            }

            var matchRows = await _context
                .Set<PropertyCustomerMatch>()
                .AsNoTracking()
                .Where(x => x.PropertyId == propertyId)
                .ToListAsync();

            var recommendations = matchRows
                .OrderByDescending(x => x.MatchScore)
                .ThenByDescending(x => x.UpdatedAt ?? x.CreatedAt)
                .Take(10)
                .Select(x => new BulkMatchRecommendation
                {
                    MatchId = x.Id,
                    PropertyId = x.PropertyId,
                    LeadId = x.LeadId,
                    Score = x.MatchScore,
                    Status = x.Status,
                    Reason = x.MatchReason
                })
                .ToList();

            return new BulkPropertyMatchResult
            {
                PropertyId = propertyId,
                ProcessedLeadCount = leadIds.Count,
                SuccessfulMatchCount = successfulMatchCount,
                FailedMatchCount = failed.Count,
                Recommendations = recommendations,
                Failures = failed
            };
        }
    }

    public class BulkLeadMatchResult
    {
        public int LeadId { get; set; }
        public int ProcessedPropertyCount { get; set; }
        public int SuccessfulMatchCount { get; set; }
        public int FailedMatchCount { get; set; }

        public List<BulkMatchRecommendation> Recommendations
        {
            get;
            set;
        } = new();

        public List<BulkMatchFailure> Failures
        {
            get;
            set;
        } = new();
    }

    public class BulkPropertyMatchResult
    {
        public int PropertyId { get; set; }
        public int ProcessedLeadCount { get; set; }
        public int SuccessfulMatchCount { get; set; }
        public int FailedMatchCount { get; set; }

        public List<BulkMatchRecommendation> Recommendations
        {
            get;
            set;
        } = new();

        public List<BulkPropertyLeadFailure> Failures
        {
            get;
            set;
        } = new();
    }

    public class BulkMatchRecommendation
    {
        public int MatchId { get; set; }
        public int PropertyId { get; set; }
        public int LeadId { get; set; }
        public decimal Score { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
    }

    public class BulkMatchFailure
    {
        public int PropertyId { get; set; }
        public string Error { get; set; } = string.Empty;
    }

    public class BulkPropertyLeadFailure
    {
        public int LeadId { get; set; }
        public string Error { get; set; } = string.Empty;
    }
}
