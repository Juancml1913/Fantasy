using Fantasy.Backend.Data;
using Fantasy.Backend.Repositories.Interfaces;
using Fantasy.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fantasy.Backend.Repositories.Implementations;

public class TeamsRepository : GenericRepository<Team>, ITeamsRepository
{
    private readonly DataContext _context;

    public TeamsRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Team>> GetAllWithCountryAsync()
    {
        return await _context.Teams
            .Include(t => t.Country)
            .OrderBy(t => t.Name)
            .ToListAsync();
    }

    public async Task<Team?> GetWithCountryAsync(int id)
    {
        return await _context.Teams
            .Include(t => t.Country)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<Team>> GetByCountryAsync(int countryId)
    {
        return await _context.Teams
            .Include(t => t.Country)
            .Where(t => t.CountryId == countryId)
            .OrderBy(t => t.Name)
            .ToListAsync();
    }
}
