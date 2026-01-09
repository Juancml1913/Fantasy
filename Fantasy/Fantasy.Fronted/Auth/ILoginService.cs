namespace Fantasy.Fronted.Auth;

public interface ILoginService
{
    Task LoginAsync(string token);
    Task LogoutAsync();
}
