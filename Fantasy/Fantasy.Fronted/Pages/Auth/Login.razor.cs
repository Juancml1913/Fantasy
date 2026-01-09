using Fantasy.Fronted.Auth;
using Fantasy.Fronted.Repositories;
using Fantasy.Shared.DTOs;
using Fantasy.Shared.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace Fantasy.Fronted.Pages.Auth;

public partial class Login
{
    [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private ILoginService LoginService { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    private LoginDTO LoginModel { get; set; } = new();
    private string? ErrorMessage { get; set; }
    private bool IsLoading { get; set; }

    private async Task HandleLogin()
    {
        IsLoading = true;
        ErrorMessage = null;

        var response = await Repository.PostAsync<LoginDTO, TokenDTO>("api/accounts/login", LoginModel);

        if (response.Error)
        {
            ErrorMessage = Localizer["InvalidCredentials"];
            IsLoading = false;
            return;
        }

        await LoginService.LoginAsync(response.Response!.Token);
        NavigationManager.NavigateTo("/");
    }
}
