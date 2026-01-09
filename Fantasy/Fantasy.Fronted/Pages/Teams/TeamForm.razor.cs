using Fantasy.Shared.Entities;
using Fantasy.Shared.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace Fantasy.Fronted.Pages.Teams;

public partial class TeamForm
{
    [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;

    [Parameter, EditorRequired] public Team Team { get; set; } = null!;
    [Parameter, EditorRequired] public List<Country>? Countries { get; set; }
    [Parameter, EditorRequired] public EventCallback OnValidSubmit { get; set; }
    [Parameter] public string? ErrorMessage { get; set; }
    [Parameter] public bool IsLoading { get; set; }
}
