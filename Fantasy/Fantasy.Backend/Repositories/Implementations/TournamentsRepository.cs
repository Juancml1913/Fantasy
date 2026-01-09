using Fantasy.Backend.Data;
using Fantasy.Backend.Repositories.Interfaces;
using Fantasy.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fantasy.Backend.Repositories.Implementations;

public class TournamentsRepository : GenericRepository<Tournament>, ITournamentsRepository
{
    private readonly DataContext _context;

    public TournamentsRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Tournament>> GetActiveAsync()
    {
        return await _context.Tournaments
            .Where(t => t.IsActive)
            .OrderBy(t => t.Name)
            .ToListAsync();
    }

    public async Task<Tournament?> GetWithTeamsAsync(int id)
    {
        return await _context.Tournaments
            .Include(t => t.TournamentTeams)
                .ThenInclude(tt => tt.Team)
                    .ThenInclude(team => team.Country)
            .FirstOrDefaultAsync(t => t.Id == id);
    }
}
