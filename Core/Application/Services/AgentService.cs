
using Microsoft.EntityFrameworkCore;

using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Core.Application.Services;

public class AgentService

{

    private readonly AppDbContext _context;

    public AgentService(AppDbContext context)

    {

        _context = context;

    }

    public async Task<Agent> CreateAgent(

        string fullName,

        string email,

        string phone)

    {

        var agent = new Agent

        {

            FullName = fullName,

            Email = email,

            Phone = phone,

            CreatedAt = DateTime.UtcNow,

            IsActive = true

        };

        _context.Agents.Add(agent);

        await _context.SaveChangesAsync();

        return agent;

    }

}

