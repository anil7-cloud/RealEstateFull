using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Controllers;

[ApiController]
[Route("api/ai/customer-ranking")]
public class AiCustomerRankingController : ControllerBase
{
    private readonly AppDbContext _context;

    public AiCustomerRankingController(AppDbContext context)
    {
        _context = context;
    }


    [HttpGet]
    public async Task<IActionResult> GetCustomerRanking()
    {
        var customers = await _context.Users
            .Select(x => new
            {
                Name = x.FirstName + " " + x.LastName,

                Score =
                    (x.PhoneNumber != null ? 20 : 0) +
                    (x.Email != null ? 20 : 0),

                Probability =
                    x.CreatedAt < DateTime.UtcNow.AddMonths(-1)
                    ? 75
                    : 50
            })
            .OrderByDescending(x => x.Score)
            .ToListAsync();


        return Ok(customers);
    }
}
