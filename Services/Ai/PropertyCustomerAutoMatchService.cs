using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;
using REAL_ESTATE_CLEAN.Core.Application.Services;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerAutoMatchService
    {
        private readonly IPropertyCustomerMatchService _matchService;
        private readonly PropertyCustomerMatchScoringService _scoringService;
        private readonly PropertyCustomerMatchHistoryService _historyService;
        private readonly AppDbContext _context;

        public PropertyCustomerAutoMatchService(
            IPropertyCustomerMatchService matchService,
            PropertyCustomerMatchScoringService scoringService,
            PropertyCustomerMatchHistoryService historyService,
            AppDbContext context)
        {
            _matchService = matchService;
            _scoringService = scoringService;
            _historyService = historyService;
            _context = context;
        }

        public async Task<PropertyCustomerMatchDto> CreateMatchAsync(
            int propertyId,
            int leadId,
            PropertyMatchScoreRequest scoreRequest)
        {
            if (propertyId <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(propertyId),
                    "PropertyId sıfırdan büyük olmalıdır.");
            }

            if (leadId <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(leadId),
                    "LeadId sıfırdan büyük olmalıdır.");
            }

            ArgumentNullException.ThrowIfNull(scoreRequest);

            var scoreResult = _scoringService.Calculate(scoreRequest);
            var newStatus = GetStatus(scoreResult.Score);

            await using var transaction =
                await _context.Database.BeginTransactionAsync();

            try
            {
                var existingMatch =
                    await _matchService.GetByPropertyAndLeadAsync(
                        propertyId,
                        leadId);

                var match = new PropertyCustomerMatchDto
                {
                    PropertyId = propertyId,
                    LeadId = leadId,
                    MatchScore = scoreResult.Score,
                    PriceMatched = scoreRequest.PriceMatched,
                    LocationMatched = scoreRequest.LocationMatched,
                    PropertyTypeMatched = scoreRequest.PropertyTypeMatched,
                    RoomCountMatched = scoreRequest.RoomCountMatched,
                    SizeMatched = scoreRequest.SizeMatched,
                    MatchReason = scoreResult.Reason,
                    Status = newStatus
                };

                if (existingMatch != null)
                {
                    var hasChanges =
                        HasChanges(
                            existingMatch,
                            match);

                    if (!hasChanges)
                    {
                        await transaction.CommitAsync();

                        return existingMatch;
                    }

                    var previousScore =
                        existingMatch.MatchScore;

                    var previousStatus =
                        existingMatch.Status;

                    match.Id =
                        existingMatch.Id;

                    match.CreatedAt =
                        existingMatch.CreatedAt;

                    match.UpdatedAt =
                        DateTime.UtcNow;

                    var updated =
                        await _matchService.UpdateAsync(
                            existingMatch.Id,
                            match);

                    if (updated == null)
                    {
                        throw new InvalidOperationException(
                            "Mevcut eşleşme güncellenemedi.");
                    }

                    var scoreChanged =
                        previousScore != updated.MatchScore;

                    var statusChanged =
                        !string.Equals(
                            previousStatus,
                            updated.Status,
                            StringComparison.Ordinal);

                    if (scoreChanged || statusChanged)
                    {
                        var changeType =
                            scoreChanged && statusChanged
                                ? "ScoreAndStatusChanged"
                                : scoreChanged
                                    ? "ScoreChanged"
                                    : "StatusChanged";

                        var description =
                            $"Eşleşme güncellendi. " +
                            $"Skor: {previousScore} -> {updated.MatchScore}, " +
                            $"Durum: {previousStatus} -> {updated.Status}.";

                        await _historyService.AddAsync(
                            updated.Id,
                            updated.PropertyId,
                            updated.LeadId,
                            previousScore,
                            updated.MatchScore,
                            previousStatus,
                            updated.Status,
                            changeType,
                            description,
                            "AutoMatch");
                    }

                    await transaction.CommitAsync();

                    return updated;
                }

                match.CreatedAt =
                    DateTime.UtcNow;

                var created =
                    await _matchService.CreateAsync(match);

                await _historyService.AddAsync(
                    created.Id,
                    created.PropertyId,
                    created.LeadId,
                    null,
                    created.MatchScore,
                    string.Empty,
                    created.Status,
                    "Created",
                    $"Yeni eşleşme oluşturuldu. " +
                    $"Skor: {created.MatchScore}, " +
                    $"Durum: {created.Status}.",
                    "AutoMatch");

                await transaction.CommitAsync();

                return created;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<List<PropertyCustomerMatchDto>>
            GetLeadRecommendationsAsync(
                int leadId,
                int limit = 10)
        {
            if (leadId <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(leadId));
            }

            return await _matchService
                .GetBestMatchesForLeadAsync(
                    leadId,
                    limit);
        }

        public async Task<List<PropertyCustomerMatchDto>>
            GetPropertyLeadRecommendationsAsync(
                int propertyId,
                int limit = 10)
        {
            if (propertyId <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(propertyId));
            }

            return await _matchService
                .GetBestLeadsForPropertyAsync(
                    propertyId,
                    limit);
        }

        private static bool HasChanges(
            PropertyCustomerMatchDto existing,
            PropertyCustomerMatchDto current)
        {
            return
                existing.MatchScore != current.MatchScore ||
                existing.PriceMatched != current.PriceMatched ||
                existing.LocationMatched != current.LocationMatched ||
                existing.PropertyTypeMatched != current.PropertyTypeMatched ||
                existing.RoomCountMatched != current.RoomCountMatched ||
                existing.SizeMatched != current.SizeMatched ||
                !string.Equals(
                    existing.MatchReason,
                    current.MatchReason,
                    StringComparison.Ordinal) ||
                !string.Equals(
                    existing.Status,
                    current.Status,
                    StringComparison.Ordinal);
        }

        private static string GetStatus(decimal score)
        {
            return score switch
            {
                >= 90 => "Hot",
                >= 75 => "High",
                >= 60 => "Medium",
                >= 40 => "Low",
                _ => "VeryLow"
            };
        }
    }
}
