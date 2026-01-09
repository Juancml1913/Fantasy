using Fantasy.Fronted.Repositories;
using Fantasy.Shared.DTOs;
using Fantasy.Shared.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace Fantasy.Fronted.Pages.Auth;

public partial class Register
{
    [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    private RegisterDTO RegisterModel { get; set; } = new();
    private string? ErrorMessage { get; set; }
    private string? SuccessMessage { get; set; }
    private bool IsLoading { get; set; }

    private async Task HandleRegister()
    {
        IsLoading = true;
        ErrorMessage = null;
        SuccessMessage = null;

        var response = await Repository.PostAsync("api/accounts/register", RegisterModel);

        if (response.Error)
        {
            ErrorMessage = await response.GetErrorMessageAsync();
            IsLoading = false;
            return;
        }

        SuccessMessage = Localizer["RegisterSuccess"];
        IsLoading = false;

        await Task.Delay(2000);
        NavigationManager.NavigateTo("/login");
    }
}
