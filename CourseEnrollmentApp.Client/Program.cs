using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using CourseEnrollmentApp.Client;
using CourseEnrollmentApp.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// --- AUTHENTICATION SERVICES (REQUIRED) ---
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<TokenService>();

// HttpClient
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:5219/")
});

// API Services
builder.Services.AddScoped<AuthApiService>();
builder.Services.AddScoped<CourseApiService>();
builder.Services.AddScoped<StudentApiService>();
builder.Services.AddScoped<HomeApiService>();

await builder.Build().RunAsync();
