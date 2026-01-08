using System.ComponentModel.DataAnnotations;

namespace Fantasy.Shared.DTOs;

public class ResetPasswordDTO
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public string Token { get; set; } = null!;

    [Required]
    [MinLength(6)]
    public string NewPassword { get; set; } = null!;

    [Required]
    [Compare("NewPassword")]
    public string ConfirmNewPassword { get; set; } = null!;
}
