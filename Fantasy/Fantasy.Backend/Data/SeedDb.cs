using Fantasy.Shared.Entities;
using Fantasy.Shared.Enums;
using Microsoft.AspNetCore.Identity;

namespace Fantasy.Backend.Data;

public class SeedDb
{
    private readonly DataContext _context;
    private readonly UserManager<User> _userManager;

    public SeedDb(DataContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task SeedAsync()
    {
        await _context.Database.EnsureCreatedAsync();
        await SeedUsersAsync();
        await SeedCountriesAsync();
    }

    private async Task SeedUsersAsync()
    {
        var adminEmail = "admin@fantasy.com";
        var user = await _userManager.FindByEmailAsync(adminEmail);

        if (user == null)
        {
            user = new User
            {
                Email = adminEmail,
                UserName = adminEmail,
                FirstName = "Admin",
                LastName = "Fantasy",
                UserType = UserType.Admin
            };

            await _userManager.CreateAsync(user, "123456");
        }
    }

    private async Task SeedCountriesAsync()
    {
        if (!_context.Countries.Any())
        {
            _context.Countries.AddRange(
                new Country { Name = "Colombia" },
                new Country { Name = "Argentina" },
                new Country { Name = "Brasil" },
                new Country { Name = "España" },
                new Country { Name = "Alemania" },
                new Country { Name = "Francia" },
                new Country { Name = "Italia" },
                new Country { Name = "Inglaterra" },
                new Country { Name = "Portugal" },
                new Country { Name = "Holanda" }
            );
            await _context.SaveChangesAsync();
        }
    }
}
