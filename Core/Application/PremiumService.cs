using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application;

public class PremiumService
{
    private readonly AppDbContext _db;

    public PremiumService(AppDbContext db)
    {
        _db = db;
    }

    public void Promote(int propertyId, int days, decimal amount)
    {
        var property = _db.Properties.FirstOrDefault(x => x.Id == propertyId);

        if (property == null) return;

        property.IsPremium = true;

        _db.SaveChanges();
    }
}
