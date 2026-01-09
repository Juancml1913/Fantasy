using Fantasy.Fronted.Repositories;
using Fantasy.Shared.DTOs;
using Fantasy.Shared.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace Fantasy.Fronted.Pages.Auth;

[Authorize]
public partial class Profile
{
    [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;

    private UserDTO? UserProfile { get; set; }
    private ChangePasswordDTO PasswordModel { get; set; } = new();

    private string? ErrorMessage { get; set; }
    private string? SuccessMessage { get; set; }
    private bool IsLoading { get; set; }

    private string? PasswordErrorMessage { get; set; }
    private string? PasswordSuccessMessage { get; set; }
    private bool IsPasswordLoading { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await LoadProfile();
    }

    private async Task LoadProfile()
    {
        var response = await Repository.GetAsync<UserDTO>("api/accounts/profile");
        if (!response.Error)
        {
            UserProfile = response.Response;
        }
    }

    private async Task HandleUpdateProfile()
    {
        IsLoading = true;
        ErrorMessage = null;
        SuccessMessage = null;

        var response = await Repository.PutAsync("api/accounts/profile", UserProfile!);

        if (response.Error)
        {
            ErrorMessage = await response.GetErrorMessageAsync();
        }
        else
        {
            SuccessMessage = Localizer["ProfileUpdated"];
        }

        IsLoading = false;
    }

    private async Task HandleChangePassword()
    {
        IsPasswordLoading = true;
        PasswordErrorMessage = null;
        PasswordSuccessMessage = null;

        var response = await Repository.PostAsync("api/accounts/changepassword", PasswordModel);

        if (response.Error)
        {
            PasswordErrorMessage = await response.GetErrorMessageAsync();
        }
        else
        {
            PasswordSuccessMessage = Localizer["PasswordChanged"];
            PasswordModel = new ChangePasswordDTO();
        }

        IsPasswordLoading = false;
    }
}
