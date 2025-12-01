namespace CourseEnrollmentApp.Application.DTOs;

public class CourseDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public List<int> EnrolledStudentIds { get; set; } = new();
}
