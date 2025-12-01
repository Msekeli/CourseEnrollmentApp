using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;

namespace CourseEnrollmentApp.Client.Services;

public class TokenService
{
    private readonly IJSRuntime _js;

    public TokenService(IJSRuntime js)
    {
        _js = js;
    }

    public bool IsLoggedIn => !string.IsNullOrEmpty(_token);

    public string UserEmail { get; private set; } = "";
    public string Initials { get; private set; } = "";

    private string? _token;

    public async Task SetTokenAsync(string token)
    {
        _token = token;
        await _js.InvokeVoidAsync("localStorage.setItem", "authToken", token);

        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);

        UserEmail = jwt.Claims.FirstOrDefault(c => c.Type == "email")?.Value ?? "";
        Initials = UserEmail.Length >= 2 
            ? string.Concat(UserEmail[0], UserEmail[1]).ToUpper()
            : "U";
    }

    public async Task LoadTokenAsync()
    {
        _token = await _js.InvokeAsync<string>("localStorage.getItem", "authToken");
    }

    public async void Logout()
    {
        _token = null;
        UserEmail = "";
        Initials = "";
        await _js.InvokeVoidAsync("localStorage.removeItem", "authToken");
    }

    public string? GetToken() => _token;
}
