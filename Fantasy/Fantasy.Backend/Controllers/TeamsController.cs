using Fantasy.Backend.UnitsOfWork.Interfaces;
using Fantasy.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fantasy.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public TeamsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var teams = await _unitOfWork.Teams.GetAllWithCountryAsync();
        return Ok(teams);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var team = await _unitOfWork.Teams.GetWithCountryAsync(id);
        if (team == null)
        {
            return NotFound();
        }
        return Ok(team);
    }

    [HttpGet("bycountry/{countryId}")]
    public async Task<IActionResult> GetByCountryAsync(int countryId)
    {
        var teams = await _unitOfWork.Teams.GetByCountryAsync(countryId);
        return Ok(teams);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> PostAsync(Team team)
    {
        var country = await _unitOfWork.Countries.GetByIdAsync(team.CountryId);
        if (country == null)
        {
            return BadRequest("Invalid country");
        }

        await _unitOfWork.Teams.AddAsync(team);
        await _unitOfWork.SaveChangesAsync();
        return Ok(team);
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> PutAsync(Team team)
    {
        var currentTeam = await _unitOfWork.Teams.GetByIdAsync(team.Id);
        if (currentTeam == null)
        {
            return NotFound();
        }

        var country = await _unitOfWork.Countries.GetByIdAsync(team.CountryId);
        if (country == null)
        {
            return BadRequest("Invalid country");
        }

        currentTeam.Name = team.Name;
        currentTeam.Image = team.Image;
        currentTeam.CountryId = team.CountryId;

        _unitOfWork.Teams.Update(currentTeam);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var team = await _unitOfWork.Teams.GetByIdAsync(id);
        if (team == null)
        {
            return NotFound();
        }

        _unitOfWork.Teams.Delete(team);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }
}
