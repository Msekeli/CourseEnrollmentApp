namespace CourseEnrollmentApp.Application.DTOs;

public class StudentDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public List<int> EnrolledCourseIds { get; set; } = new();
}
