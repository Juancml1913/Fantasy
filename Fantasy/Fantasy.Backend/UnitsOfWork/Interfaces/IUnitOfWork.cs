using Fantasy.Backend.Repositories.Interfaces;

namespace Fantasy.Backend.UnitsOfWork.Interfaces;

public interface IUnitOfWork : IDisposable
{
    ICountriesRepository Countries { get; }
    ITeamsRepository Teams { get; }
    ITournamentsRepository Tournaments { get; }

    Task<int> SaveChangesAsync();
}
