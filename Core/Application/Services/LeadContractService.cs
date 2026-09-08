using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class LeadContractService
{
    private readonly AppDbContext _context;

    public LeadContractService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<LeadContract> CreateAsync(
        int leadId,
        int propertyId,
        string contractType,
        decimal amount)
    {
        var contract = new LeadContract
        {
            LeadId = leadId,
            PropertyId = propertyId,
            ContractType = contractType,
            ContractNumber = Guid.NewGuid().ToString(),
            Amount = amount,
            Currency = "TRY",
            StartDate = DateTime.UtcNow,
            Status = "Draft",
            FilePath = string.Empty,
            Notes = string.Empty,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.LeadContracts.Add(contract);

        await _context.SaveChangesAsync();

        return contract;
    }


    public async Task<List<LeadContract>> GetByLeadAsync(
        int leadId)
    {
        return await _context.LeadContracts
            .Where(x => x.LeadId == leadId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<bool> UpdateStatusAsync(
        int id,
        string status)
    {
        var contract = await _context.LeadContracts
            .FirstOrDefaultAsync(x => x.Id == id);

        if (contract == null)
            return false;

        contract.Status = status;
        contract.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}
