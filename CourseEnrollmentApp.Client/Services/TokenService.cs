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

    private string? _token;

    public bool IsLoggedIn => !string.IsNullOrEmpty(_token);

    public string UserEmail { get; private set; } = "";
    public string Initials { get; private set; } = "";
    public int UserId { get; private set; }

    // ------------------------------
    // STORE TOKEN + PARSE CLAIMS
    // ------------------------------
    public async Task SetTokenAsync(string token)
    {
        _token = token;
        await _js.InvokeVoidAsync("localStorage.setItem", "authToken", token);

        ParseToken(token);
    }

    // ------------------------------
    // LOAD TOKEN FROM STORAGE
    // ------------------------------
    public async Task LoadTokenAsync()
    {
        _token = await _js.InvokeAsync<string>("localStorage.getItem", "authToken");

        if (!string.IsNullOrEmpty(_token))
            ParseToken(_token);
    }

    // ------------------------------
    // REMOVE TOKEN
    // ------------------------------
    public async Task LogoutAsync()
    {
        _token = null;
        UserEmail = "";
        Initials = "";
        UserId = 0;

        await _js.InvokeVoidAsync("localStorage.removeItem", "authToken");
    }

    // ------------------------------
    // GET TOKEN
    // ------------------------------
    public string? GetToken() => _token;

    // ------------------------------
    // PARSE CLAIMS FROM JWT
    // ------------------------------
    private void ParseToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);

        // Email (tries all possible claim names)
        UserEmail =
            jwt.Claims.FirstOrDefault(c => c.Type == "email")?.Value ??
            jwt.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value ??
            jwt.Claims.FirstOrDefault(c => c.Type == "name")?.Value ??
            "";

        // User ID (tries multiple typical claim names)
        var idValue =
            jwt.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value ??
            jwt.Claims.FirstOrDefault(c => c.Type == "sub")?.Value ??
            jwt.Claims.FirstOrDefault(c => c.Type == "userid")?.Value;

        UserId = int.TryParse(idValue, out var parsedId) ? parsedId : 0;

        // Initials
        Initials = !string.IsNullOrEmpty(UserEmail) && UserEmail.Length >= 2
            ? $"{UserEmail[0]}{UserEmail[1]}".ToUpper()
            : "U";
    }
}
