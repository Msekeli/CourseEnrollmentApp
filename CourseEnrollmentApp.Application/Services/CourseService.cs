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

        return new CourseDto
        {
            Id = course.Id,
            Title = course.Title,
            Description = course.Description,
            EnrolledStudentIds = course.Enrollments.Select(e => e.StudentId).ToList()
        };
    }

    public async Task<List<CourseDto>> GetAllCoursesAsync()
    {
        var courses = await _courseRepo.GetAllAsync();

        return courses.Select(course => new CourseDto
        {
            Id = course.Id,
            Title = course.Title,
            Description = course.Description,
            EnrolledStudentIds = course.Enrollments.Select(e => e.StudentId).ToList()
        }).ToList();
    }

    public async Task<CourseDto> CreateCourseAsync(CourseDto dto)
    {
        var course = new Course
        {
            Title = dto.Title,
            Description = dto.Description
        };

        await _courseRepo.AddAsync(course);

        return new CourseDto
        {
            Id = course.Id,
            Title = course.Title,
            Description = course.Description,
            EnrolledStudentIds = new()
        };
    }
}
