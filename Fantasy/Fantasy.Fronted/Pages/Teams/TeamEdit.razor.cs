using Fantasy.Fronted.Repositories;
using Fantasy.Shared.Entities;
using Fantasy.Shared.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace Fantasy.Fronted.Pages.Teams;

public partial class TeamEdit
{
    [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    [Parameter] public int Id { get; set; }

    private Team? Team { get; set; }
    private List<Country>? Countries { get; set; }
    private string? ErrorMessage { get; set; }
    private bool IsLoading { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var countriesResponse = await Repository.GetAsync<List<Country>>("api/countries");
        Countries = countriesResponse.Response;

        var teamResponse = await Repository.GetAsync<Team>($"api/teams/{Id}");
        Team = teamResponse.Response;
    }

    private async Task UpdateAsync()
    {
        IsLoading = true;
        ErrorMessage = null;

        var response = await Repository.PutAsync("api/teams", Team!);

        if (response.Error)
        {
            ErrorMessage = await response.GetErrorMessageAsync();
            IsLoading = false;
            return;
        }

        NavigationManager.NavigateTo("/teams");
    }
}
