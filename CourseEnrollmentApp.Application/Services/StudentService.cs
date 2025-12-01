using CourseEnrollmentApp.Application.DTOs;
using CourseEnrollmentApp.Application.Interfaces;
using CourseEnrollmentApp.Domain.Entities;

namespace CourseEnrollmentApp.Application.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepo;
    private readonly ICourseRepository _courseRepo;

    public StudentService(IStudentRepository studentRepo, ICourseRepository courseRepo)
    {
        _studentRepo = studentRepo;
        _courseRepo = courseRepo;
    }

    public async Task<StudentDto?> GetStudentByIdAsync(int id)
    {
        var student = await _studentRepo.GetByIdAsync(id);
        if (student == null) return null;

        return new StudentDto
        {
            Id = student.Id,
            Name = student.Name,
            Email = student.Email,
            EnrolledCourseIds = student.Enrollments.Select(e => e.CourseId).ToList()
        };
    }

    public async Task<List<StudentDto>> GetAllStudentsAsync()
    {
        var students = await _studentRepo.GetAllAsync();

        return students.Select(student => new StudentDto
        {
            Id = student.Id,
            Name = student.Name,
            Email = student.Email,
            EnrolledCourseIds = student.Enrollments.Select(e => e.CourseId).ToList()
        }).ToList();
    }

    public async Task<StudentDto> RegisterStudentAsync(RegisterDto dto)
    {
        var student = new Student
        {
            Name = dto.Name,
            Email = dto.Email
        };

        await _studentRepo.AddAsync(student);

        return new StudentDto
        {
            Id = student.Id,
            Name = student.Name,
            Email = student.Email,
            EnrolledCourseIds = new()
        };
    }

    public async Task EnrollStudentAsync(int studentId, int courseId)
    {
        var student = await _studentRepo.GetByIdAsync(studentId);
        var course = await _courseRepo.GetByIdAsync(courseId);

        if (student == null || course == null)
            return;

        // Already enrolled check
        if (student.Enrollments.Any(e => e.CourseId == courseId))
            return;

        student.Enrollments.Add(new Enrollment
        {
            StudentId = studentId,
            CourseId = courseId
        });

        await _studentRepo.UpdateAsync(student);
    }

    public async Task UnenrollStudentAsync(int studentId, int courseId)
    {
        var student = await _studentRepo.GetByIdAsync(studentId);
        if (student == null) return;

        var enrollment = student.Enrollments
            .FirstOrDefault(e => e.CourseId == courseId);

        if (enrollment == null) return;

        student.Enrollments.Remove(enrollment);

        await _studentRepo.UpdateAsync(student);
    }

    public async Task<List<CourseDto>> GetStudentCoursesAsync(int studentId)
    {
        var student = await _studentRepo.GetByIdAsync(studentId);
        if (student == null) return new();

        return student.Enrollments
            .Select(e => new CourseDto
            {
                Id = e.CourseId,
                Title = e.Course?.Title ?? "",
                Description = e.Course?.Description ?? "",
                EnrolledStudentIds = e.Course?.Enrollments.Select(x => x.StudentId).ToList() ?? new()
            })
            .ToList();
    }
}
