using Blazored.LocalStorage;
using Fantasy.Fronted;
using Fantasy.Fronted.Auth;
using Fantasy.Fronted.Repositories;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Get API URL from configuration
var apiBaseUrl = builder.Configuration.GetValue<string>("ApiSettings:BaseUrl")
    ?? "https://localhost:7128";

// Configure HttpClient with IHttpClientFactory pattern
builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

// Register HttpClient for injection (uses the named client)
builder.Services.AddScoped(sp =>
{
    var factory = sp.GetRequiredService<IHttpClientFactory>();
    return factory.CreateClient("API");
});

builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddLocalization();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationProviderJWT>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationProviderJWT>(provider =>
    provider.GetRequiredService<AuthenticationProviderJWT>());
builder.Services.AddScoped<ILoginService, AuthenticationProviderJWT>(provider =>
    provider.GetRequiredService<AuthenticationProviderJWT>());

await builder.Build().RunAsync();
