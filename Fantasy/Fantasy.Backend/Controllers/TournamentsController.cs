using Fantasy.Backend.UnitsOfWork.Interfaces;
using Fantasy.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fantasy.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TournamentsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public TournamentsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var tournaments = await _unitOfWork.Tournaments.GetAllAsync();
        return Ok(tournaments);
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetActiveAsync()
    {
        var tournaments = await _unitOfWork.Tournaments.GetActiveAsync();
        return Ok(tournaments);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var tournament = await _unitOfWork.Tournaments.GetByIdAsync(id);
        if (tournament == null)
        {
            return NotFound();
        }
        return Ok(tournament);
    }

    [HttpGet("{id}/teams")]
    public async Task<IActionResult> GetWithTeamsAsync(int id)
    {
        var tournament = await _unitOfWork.Tournaments.GetWithTeamsAsync(id);
        if (tournament == null)
        {
            return NotFound();
        }
        return Ok(tournament);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> PostAsync(Tournament tournament)
    {
        await _unitOfWork.Tournaments.AddAsync(tournament);
        await _unitOfWork.SaveChangesAsync();
        return Ok(tournament);
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> PutAsync(Tournament tournament)
    {
        var currentTournament = await _unitOfWork.Tournaments.GetByIdAsync(tournament.Id);
        if (currentTournament == null)
        {
            return NotFound();
        }

        currentTournament.Name = tournament.Name;
        currentTournament.Image = tournament.Image;
        currentTournament.IsActive = tournament.IsActive;
        currentTournament.StartDate = tournament.StartDate;
        currentTournament.EndDate = tournament.EndDate;

        _unitOfWork.Tournaments.Update(currentTournament);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var tournament = await _unitOfWork.Tournaments.GetByIdAsync(id);
        if (tournament == null)
        {
            return NotFound();
        }

        _unitOfWork.Tournaments.Delete(tournament);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }

    [Authorize]
    [HttpPost("{id}/teams/{teamId}")]
    public async Task<IActionResult> AddTeamAsync(int id, int teamId)
    {
        var tournament = await _unitOfWork.Tournaments.GetByIdAsync(id);
        if (tournament == null)
        {
            return NotFound("Tournament not found");
        }

        var team = await _unitOfWork.Teams.GetByIdAsync(teamId);
        if (team == null)
        {
            return NotFound("Team not found");
        }

        var tournamentTeam = new TournamentTeam
        {
            TournamentId = id,
            TeamId = teamId
        };

        tournament.TournamentTeams.Add(tournamentTeam);
        await _unitOfWork.SaveChangesAsync();
        return Ok(tournamentTeam);
    }

    [Authorize]
    [HttpDelete("{id}/teams/{teamId}")]
    public async Task<IActionResult> RemoveTeamAsync(int id, int teamId)
    {
        var tournament = await _unitOfWork.Tournaments.GetWithTeamsAsync(id);
        if (tournament == null)
        {
            return NotFound("Tournament not found");
        }

        var tournamentTeam = tournament.TournamentTeams.FirstOrDefault(tt => tt.TeamId == teamId);
        if (tournamentTeam == null)
        {
            return NotFound("Team not in tournament");
        }

        tournament.TournamentTeams.Remove(tournamentTeam);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }
}
