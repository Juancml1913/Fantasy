using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Fantasy.Shared.Entities;

public class Team
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    public string? Image { get; set; }

    public int CountryId { get; set; }

    public Country? Country { get; set; }

    [JsonIgnore]
    public ICollection<TournamentTeam> TournamentTeams { get; set; } = new List<TournamentTeam>();
}
