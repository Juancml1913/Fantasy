using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Fantasy.Shared.Entities;

public class Tournament
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    public string? Image { get; set; }

    public bool IsActive { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    [JsonIgnore]
    public ICollection<TournamentTeam> TournamentTeams { get; set; } = new List<TournamentTeam>();
}
