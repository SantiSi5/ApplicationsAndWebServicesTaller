using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;
using Taller.Frontend.AuthenticationProviders;
using Taller.Frontend.Components;
using Taller.Frontend.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMudServices();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddSingleton(_ => new HttpClient { BaseAddress = new Uri("https://localhost:7066") });
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationProviderTest>();
builder.Services.AddScoped<IRepository, Repository>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();