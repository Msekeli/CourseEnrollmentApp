using CourseEnrollmentApp.Application.DTOs;

namespace CourseEnrollmentApp.Application.Interfaces;

public interface IStudentService
{
    Task<StudentDto?> GetStudentByIdAsync(int id);
    Task<List<StudentDto>> GetAllStudentsAsync();

    Task<StudentDto> RegisterStudentAsync(RegisterDto dto);

    Task EnrollStudentAsync(int studentId, int courseId);
    Task UnenrollStudentAsync(int studentId, int courseId);

    Task<List<CourseDto>> GetStudentCoursesAsync(int studentId);
}
