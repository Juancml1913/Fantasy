using Fantasy.Shared.Entities;

namespace Fantasy.Backend.Repositories.Interfaces;

public interface ICountriesRepository : IGenericRepository<Country>
{
    Task<Country?> GetByNameAsync(string name);
}
