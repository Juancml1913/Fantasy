using Fantasy.Backend.UnitsOfWork.Interfaces;
using Fantasy.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fantasy.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountriesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public CountriesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var countries = await _unitOfWork.Countries.GetAllAsync();
        return Ok(countries);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var country = await _unitOfWork.Countries.GetByIdAsync(id);
        if (country == null)
        {
            return NotFound();
        }
        return Ok(country);
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> PutAsync(Country country)
    {
        var currentCountry = await _unitOfWork.Countries.GetByIdAsync(country.Id);
        if (currentCountry == null)
        {
            return NotFound();
        }

        currentCountry.Name = country.Name;
        _unitOfWork.Countries.Update(currentCountry);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> PostAsync(Country country)
    {
        await _unitOfWork.Countries.AddAsync(country);
        await _unitOfWork.SaveChangesAsync();
        return Ok(country);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var country = await _unitOfWork.Countries.GetByIdAsync(id);
        if (country == null)
        {
            return NotFound();
        }

        _unitOfWork.Countries.Delete(country);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }
}
