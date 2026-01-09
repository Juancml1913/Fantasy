using Fantasy.Fronted.Repositories;
using Fantasy.Shared.Entities;
using Fantasy.Shared.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace Fantasy.Fronted.Pages.Teams;

public partial class TeamsIndex
{
    [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;

    private List<Team>? Teams { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await LoadAsync();
    }

    private async Task LoadAsync()
    {
        var response = await Repository.GetAsync<List<Team>>("api/teams");
        Teams = response.Response;
    }

    private async Task DeleteAsync(Team team)
    {
        var response = await Repository.DeleteAsync($"api/teams/{team.Id}");
        if (!response.Error)
        {
            await LoadAsync();
        }
    }
}
