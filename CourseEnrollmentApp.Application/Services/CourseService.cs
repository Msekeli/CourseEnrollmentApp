using CourseEnrollmentApp.Application.DTOs;
using CourseEnrollmentApp.Application.Interfaces;
using CourseEnrollmentApp.Domain.Entities;

namespace CourseEnrollmentApp.Application.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepo;

    public CourseService(ICourseRepository courseRepo)
    {
        _courseRepo = courseRepo;
    }

    public async Task<CourseDto?> GetCourseByIdAsync(int id)
    {
        var course = await _courseRepo.GetByIdAsync(id);
        if (course == null) return null;

        return MapToDto(course);
    }

    public async Task<List<CourseDto>> GetAllCoursesAsync()
    {
        var courses = await _courseRepo.GetAllAsync();
        return courses.Select(MapToDto).ToList();
    }

    public async Task<CourseDto> CreateCourseAsync(CourseDto dto)
    {
        var course = new Course
        {
            Title = dto.Title,
            Description = dto.Description,
            Thumbnail = dto.Thumbnail,
            Category = dto.Category
        };

        await _courseRepo.AddAsync(course);

        return MapToDto(course);
    }

    // -----------------------------
    // Mapping helper
    // -----------------------------
    private static CourseDto MapToDto(Course course)
    {
        return new CourseDto
        {
            Id = course.Id,
            Title = course.Title,
            Description = course.Description,
            Thumbnail = course.Thumbnail,
            Category = course.Category,
            EnrolledStudentIds = course.Enrollments.Select(e => e.StudentId).ToList()
        };
    }
}
