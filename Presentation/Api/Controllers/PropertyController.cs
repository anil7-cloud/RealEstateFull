using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyController : ControllerBase
{
    private readonly AppDbContext _db;

    public PropertyController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_db.Properties.ToList());
    }

    [HttpGet("filter")]
    public IActionResult Filter(string city, decimal? minPrice, decimal? maxPrice)
    {
        var query = _db.Properties.AsQueryable();

        if (!string.IsNullOrEmpty(city))
            query = query.Where(x => x.Location.Contains(city));

        if (minPrice.HasValue)
            query = query.Where(x => x.Price >= minPrice);

        if (maxPrice.HasValue)
            query = query.Where(x => x.Price <= maxPrice);

        return Ok(query.ToList());
    }
}
