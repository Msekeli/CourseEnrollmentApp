using System.Net.Http.Json;
using CourseEnrollmentApp.Application.DTOs;

namespace CourseEnrollmentApp.Client.Services;
public class AuthApiService
{
    private readonly HttpClient _http;

    public AuthApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<string?> RegisterAsync(RegisterDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/auth/register", dto);

        if (!response.IsSuccessStatusCode)
            return null;

        var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
        return result?.Token;
    }

    public async Task<string?> LoginAsync(LoginDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/auth/login", dto);

        if (!response.IsSuccessStatusCode)
            return null;

        var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
        return result?.Token;
    }
}

public class TokenResponse
{
    public string Token { get; set; } = string.Empty;
}
