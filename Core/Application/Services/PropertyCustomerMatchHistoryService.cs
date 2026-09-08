using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services
{
    public class PropertyCustomerMatchHistoryService
    {
        private readonly AppDbContext _context;

        public PropertyCustomerMatchHistoryService(
            AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<PropertyCustomerMatchHistory>>
            GetAllAsync()
        {
            return await _context
                .Set<PropertyCustomerMatchHistory>()
                .AsNoTracking()
                .OrderByDescending(x => x.ChangedAt)
                .ToListAsync();
        }

        public async Task<PropertyCustomerMatchHistory?>
            GetByIdAsync(int id)
        {
            return await _context
                .Set<PropertyCustomerMatchHistory>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<PropertyCustomerMatchHistory>>
            GetByMatchIdAsync(int matchId)
        {
            return await _context
                .Set<PropertyCustomerMatchHistory>()
                .AsNoTracking()
                .Where(x =>
                    x.PropertyCustomerMatchId == matchId)
                .OrderByDescending(x => x.ChangedAt)
                .ToListAsync();
        }

        public async Task<List<PropertyCustomerMatchHistory>>
            GetByLeadIdAsync(int leadId)
        {
            return await _context
                .Set<PropertyCustomerMatchHistory>()
                .AsNoTracking()
                .Where(x => x.LeadId == leadId)
                .OrderByDescending(x => x.ChangedAt)
                .ToListAsync();
        }

        public async Task<List<PropertyCustomerMatchHistory>>
            GetByPropertyIdAsync(int propertyId)
        {
            return await _context
                .Set<PropertyCustomerMatchHistory>()
                .AsNoTracking()
                .Where(x => x.PropertyId == propertyId)
                .OrderByDescending(x => x.ChangedAt)
                .ToListAsync();
        }

        public async Task<PropertyCustomerMatchHistory>
            AddAsync(
                int matchId,
                int propertyId,
                int leadId,
                decimal? previousScore,
                decimal newScore,
                string previousStatus,
                string newStatus,
                string changeType,
                string description,
                string changedBy = "System")
        {
            var history =
                new PropertyCustomerMatchHistory
                {
                    PropertyCustomerMatchId = matchId,
                    PropertyId = propertyId,
                    LeadId = leadId,

                    PreviousScore = previousScore,
                    NewScore = newScore,

                    PreviousStatus =
                        previousStatus ?? string.Empty,

                    NewStatus =
                        newStatus ?? string.Empty,

                    ChangeType =
                        changeType ?? string.Empty,

                    Description =
                        description ?? string.Empty,

                    ChangedBy =
                        string.IsNullOrWhiteSpace(changedBy)
                            ? "System"
                            : changedBy,

                    ChangedAt = DateTime.UtcNow
                };

            _context
                .Set<PropertyCustomerMatchHistory>()
                .Add(history);

            await _context.SaveChangesAsync();

            return history;
        }

        public async Task<PropertyCustomerMatchHistory>
            RecordStatusChangeAsync(
                PropertyCustomerMatch match,
                string previousStatus,
                string newStatus,
                string changedBy = "System")
        {
            ArgumentNullException.ThrowIfNull(match);

            return await AddAsync(
                match.Id,
                match.PropertyId,
                match.LeadId,
                match.MatchScore,
                match.MatchScore,
                previousStatus,
                newStatus,
                "StatusChanged",
                $"Eşleşme durumu {previousStatus} " +
                $"aşamasından {newStatus} aşamasına değiştirildi.",
                changedBy);
        }

        public async Task<PropertyCustomerMatchHistory>
            RecordScoreChangeAsync(
                PropertyCustomerMatch match,
                decimal previousScore,
                decimal newScore,
                string changedBy = "System")
        {
            ArgumentNullException.ThrowIfNull(match);

            return await AddAsync(
                match.Id,
                match.PropertyId,
                match.LeadId,
                previousScore,
                newScore,
                match.Status,
                match.Status,
                "ScoreChanged",
                $"Eşleşme skoru {previousScore} " +
                $"değerinden {newScore} değerine değiştirildi.",
                changedBy);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var history = await _context
                .Set<PropertyCustomerMatchHistory>()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (history == null)
                return false;

            _context
                .Set<PropertyCustomerMatchHistory>()
                .Remove(history);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
