using CourseEnrollmentApp.Application.DTOs;

namespace CourseEnrollmentApp.Application.Interfaces;

public interface ICourseService
{
    Task<CourseDto?> GetCourseByIdAsync(int id);
    Task<List<CourseDto>> GetAllCoursesAsync();

    Task<CourseDto> CreateCourseAsync(CourseDto dto);
}
