using Fantasy.Shared.Entities;

namespace Fantasy.Backend.Repositories.Interfaces;

public interface ITournamentsRepository : IGenericRepository<Tournament>
{
    Task<IEnumerable<Tournament>> GetActiveAsync();

    Task<Tournament?> GetWithTeamsAsync(int id);
}
