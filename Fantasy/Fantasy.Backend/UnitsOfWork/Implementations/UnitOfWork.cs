using Fantasy.Backend.Data;
using Fantasy.Backend.Repositories.Implementations;
using Fantasy.Backend.Repositories.Interfaces;
using Fantasy.Backend.UnitsOfWork.Interfaces;

namespace Fantasy.Backend.UnitsOfWork.Implementations;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _context;
    private ICountriesRepository? _countriesRepository;
    private ITeamsRepository? _teamsRepository;
    private ITournamentsRepository? _tournamentsRepository;

    public UnitOfWork(DataContext context)
    {
        _context = context;
    }

    public ICountriesRepository Countries => _countriesRepository ??= new CountriesRepository(_context);
    public ITeamsRepository Teams => _teamsRepository ??= new TeamsRepository(_context);
    public ITournamentsRepository Tournaments => _tournamentsRepository ??= new TournamentsRepository(_context);

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
