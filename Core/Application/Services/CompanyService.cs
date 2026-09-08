using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class CompanyService
{
    private readonly AppDbContext _context;

    public CompanyService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Company> CreateCompany(
        string name,
        string email,
        string phone)
    {
        var company = new Company
        {
            Name = name,
            Email = email,
            Phone = phone,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        _context.Companies.Add(company);

        await _context.SaveChangesAsync();

        return company;
    }
}
