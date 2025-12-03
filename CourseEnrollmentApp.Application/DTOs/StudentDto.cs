namespace CourseEnrollmentApp.Application.DTOs;

public class StudentDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";

    public List<int> EnrolledCourseIds { get; set; } = new();
}
