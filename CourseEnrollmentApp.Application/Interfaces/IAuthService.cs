using CourseEnrollmentApp.Application.DTOs;

namespace CourseEnrollmentApp.Application.Interfaces;

public interface IAuthService
{
    Task<string> RegisterAsync(RegisterDto dto);
    Task<string> LoginAsync(LoginDto dto);
}
