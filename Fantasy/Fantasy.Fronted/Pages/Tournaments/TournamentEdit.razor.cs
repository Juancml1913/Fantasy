using Fantasy.Fronted.Repositories;
using Fantasy.Shared.Entities;
using Fantasy.Shared.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace Fantasy.Fronted.Pages.Tournaments;

public partial class TournamentEdit
{
    [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    [Parameter] public int Id { get; set; }

    private Tournament? Tournament { get; set; }
    private string? ErrorMessage { get; set; }
    private bool IsLoading { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var response = await Repository.GetAsync<Tournament>($"api/tournaments/{Id}");
        Tournament = response.Response;
    }

    private async Task UpdateAsync()
    {
        IsLoading = true;
        ErrorMessage = null;

        var response = await Repository.PutAsync("api/tournaments", Tournament!);

        if (response.Error)
        {
            ErrorMessage = await response.GetErrorMessageAsync();
            IsLoading = false;
            return;
        }

        NavigationManager.NavigateTo("/tournaments");
    }
}
