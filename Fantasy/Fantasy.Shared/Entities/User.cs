using System.ComponentModel.DataAnnotations;
using Fantasy.Shared.Enums;
using Microsoft.AspNetCore.Identity;

namespace Fantasy.Shared.Entities;

public class User : IdentityUser
{
    [MaxLength(100)]
    [Required]
    public string FirstName { get; set; } = null!;

    [MaxLength(100)]
    [Required]
    public string LastName { get; set; } = null!;

    public string? Photo { get; set; }

    public UserType UserType { get; set; } = UserType.User;

    public string FullName => $"{FirstName} {LastName}";
}
