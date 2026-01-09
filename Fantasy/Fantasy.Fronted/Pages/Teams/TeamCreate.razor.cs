using Fantasy.Fronted.Repositories;
using Fantasy.Shared.Entities;
using Fantasy.Shared.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace Fantasy.Fronted.Pages.Teams;

public partial class TeamCreate
{
    [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    private Team Team { get; set; } = new();
    private List<Country>? Countries { get; set; }
    private string? ErrorMessage { get; set; }
    private bool IsLoading { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var response = await Repository.GetAsync<List<Country>>("api/countries");
        Countries = response.Response;
    }

    private async Task CreateAsync()
    {
        IsLoading = true;
        ErrorMessage = null;

        var response = await Repository.PostAsync("api/teams", Team);

        if (response.Error)
        {
            ErrorMessage = await response.GetErrorMessageAsync();
            IsLoading = false;
            return;
        }

        NavigationManager.NavigateTo("/teams");
    }
}
