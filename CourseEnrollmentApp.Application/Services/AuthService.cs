using CourseEnrollmentApp.Application.DTOs;
using CourseEnrollmentApp.Application.Interfaces;
using CourseEnrollmentApp.Domain.Entities;
using CourseEnrollmentApp.Application.Security;

namespace CourseEnrollmentApp.Application.Services;

public class AuthService : IAuthService
{
    private readonly IStudentRepository _studentRepo;
    private readonly JwtTokenGenerator _tokenGenerator;

    public AuthService(IStudentRepository studentRepo, JwtTokenGenerator tokenGenerator)
    {
        _studentRepo = studentRepo;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<string> RegisterAsync(RegisterDto dto)
    {
        var student = new Student
        {
            Name = dto.Name,
            Email = dto.Email
        };

        await _studentRepo.AddAsync(student);

        return _tokenGenerator.GenerateToken(student.Id, student.Email);
    }

    public async Task<string> LoginAsync(LoginDto dto)
    {
        var allStudents = await _studentRepo.GetAllAsync();
        var student = allStudents.FirstOrDefault(s =>
            s.Email == dto.Email);

        if (student == null)
            return string.Empty;

        return _tokenGenerator.GenerateToken(student.Id, student.Email);
    }
}
