using Fantasy.Shared.Entities;
using Fantasy.Shared.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace Fantasy.Fronted.Pages.Tournaments;

public partial class TournamentForm
{
    [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;

    [Parameter, EditorRequired] public Tournament Tournament { get; set; } = null!;
    [Parameter, EditorRequired] public EventCallback OnValidSubmit { get; set; }
    [Parameter] public string? ErrorMessage { get; set; }
    [Parameter] public bool IsLoading { get; set; }
}
