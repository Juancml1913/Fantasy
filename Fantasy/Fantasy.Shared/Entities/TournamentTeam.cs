using System.Text.Json.Serialization;

namespace Fantasy.Shared.Entities;

public class TournamentTeam
{
    public int Id { get; set; }

    public int TournamentId { get; set; }

    [JsonIgnore]
    public Tournament? Tournament { get; set; }

    public int TeamId { get; set; }

    [JsonIgnore]
    public Team? Team { get; set; }
}
