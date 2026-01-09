using Fantasy.Fronted.Repositories;
using Fantasy.Shared.Entities;
using Fantasy.Shared.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace Fantasy.Fronted.Pages.Tournaments;

public partial class TournamentsIndex
{
    [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;

    private List<Tournament>? Tournaments { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await LoadAsync();
    }

    private async Task LoadAsync()
    {
        var response = await Repository.GetAsync<List<Tournament>>("api/tournaments");
        Tournaments = response.Response;
    }

    private async Task DeleteAsync(Tournament tournament)
    {
        var response = await Repository.DeleteAsync($"api/tournaments/{tournament.Id}");
        if (!response.Error)
        {
            await LoadAsync();
        }
    }
}
