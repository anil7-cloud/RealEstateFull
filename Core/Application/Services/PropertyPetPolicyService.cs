using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class PropertyPetPolicyService
{
    private readonly AppDbContext _context;

    public PropertyPetPolicyService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PropertyPetPolicy> CreatePolicy(
        int propertyId,
        bool petsAllowed,
        bool dogsAllowed,
        bool catsAllowed,
        bool otherPetsAllowed,
        int? maximumPetCount,
        decimal petDepositAmount,
        string restrictions)
    {
        var exists = await _context.PropertyPetPolicies
            .AnyAsync(x => x.PropertyId == propertyId);

        if (exists)
        {
            throw new InvalidOperationException(
                "Bu ilan için evcil hayvan politikası zaten mevcut.");
        }

        if (maximumPetCount.HasValue && maximumPetCount.Value < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(maximumPetCount));
        }

        if (petDepositAmount < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(petDepositAmount));
        }

        var policy = new PropertyPetPolicy
        {
            PropertyId = propertyId,
            PetsAllowed = petsAllowed,
            DogsAllowed = petsAllowed && dogsAllowed,
            CatsAllowed = petsAllowed && catsAllowed,
            OtherPetsAllowed = petsAllowed && otherPetsAllowed,
            MaximumPetCount = petsAllowed ? maximumPetCount : 0,
            PetDepositAmount = petsAllowed ? petDepositAmount : 0,
            Restrictions = restrictions.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        _context.PropertyPetPolicies.Add(policy);
        await _context.SaveChangesAsync();

        return policy;
    }

    public async Task<PropertyPetPolicy?> GetPolicy(
        int propertyId)
    {
        return await _context.PropertyPetPolicies
            .FirstOrDefaultAsync(x => x.PropertyId == propertyId);
    }

    public async Task<bool> UpdatePolicy(
        int propertyId,
        bool petsAllowed,
        bool dogsAllowed,
        bool catsAllowed,
        bool otherPetsAllowed,
        int? maximumPetCount,
        decimal petDepositAmount,
        string restrictions)
    {
        var policy = await _context.PropertyPetPolicies
            .FirstOrDefaultAsync(x => x.PropertyId == propertyId);

        if (policy is null)
            return false;

        policy.PetsAllowed = petsAllowed;
        policy.DogsAllowed = petsAllowed && dogsAllowed;
        policy.CatsAllowed = petsAllowed && catsAllowed;
        policy.OtherPetsAllowed = petsAllowed && otherPetsAllowed;
        policy.MaximumPetCount = petsAllowed ? maximumPetCount : 0;
        policy.PetDepositAmount = petsAllowed ? petDepositAmount : 0;
        policy.Restrictions = restrictions.Trim();
        policy.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<PropertyPetPolicy>> GetPetFriendlyProperties()
    {
        return await _context.PropertyPetPolicies
            .Where(x => x.PetsAllowed)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<bool> DeletePolicy(int id)
    {
        var policy = await _context.PropertyPetPolicies
            .FirstOrDefaultAsync(x => x.Id == id);

        if (policy is null)
            return false;

        _context.PropertyPetPolicies.Remove(policy);
        await _context.SaveChangesAsync();

        return true;
    }
}
