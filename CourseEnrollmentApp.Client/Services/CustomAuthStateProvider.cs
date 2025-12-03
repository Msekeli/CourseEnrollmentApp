using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace CourseEnrollmentApp.Client.Services;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly TokenService _tokenService;

    public CustomAuthStateProvider(TokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        await _tokenService.LoadTokenAsync();

        if (!_tokenService.IsLoggedIn)
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, _tokenService.UserEmail),
            new Claim(ClaimTypes.NameIdentifier, _tokenService.UserId.ToString())
        };

        var identity = new ClaimsIdentity(claims, "jwt");
        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    public void NotifyAuthChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}
