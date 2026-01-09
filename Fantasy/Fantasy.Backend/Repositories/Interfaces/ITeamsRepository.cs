using Fantasy.Shared.Entities;

namespace Fantasy.Backend.Repositories.Interfaces;

public interface ITeamsRepository : IGenericRepository<Team>
{
    Task<IEnumerable<Team>> GetAllWithCountryAsync();

    Task<Team?> GetWithCountryAsync(int id);

    Task<IEnumerable<Team>> GetByCountryAsync(int countryId);
}
