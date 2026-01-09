using Fantasy.Fronted.Repositories;
using Fantasy.Shared.Entities;
using Fantasy.Shared.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace Fantasy.Fronted.Pages.Tournaments;

public partial class TournamentCreate
{
    [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    private Tournament Tournament { get; set; } = new()
    {
        StartDate = DateTime.Today,
        EndDate = DateTime.Today.AddMonths(1)
    };
    private string? ErrorMessage { get; set; }
    private bool IsLoading { get; set; }

    private async Task CreateAsync()
    {
        IsLoading = true;
        ErrorMessage = null;

        var response = await Repository.PostAsync("api/tournaments", Tournament);

        if (response.Error)
        {
            ErrorMessage = await response.GetErrorMessageAsync();
            IsLoading = false;
            return;
        }

        NavigationManager.NavigateTo("/tournaments");
    }
}
