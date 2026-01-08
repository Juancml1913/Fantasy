using Fantasy.Shared.Enums;

namespace Fantasy.Shared.DTOs;

public class UserDTO
{
    public string Id { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Photo { get; set; }
    public UserType UserType { get; set; }
    public string FullName => $"{FirstName} {LastName}";
}
