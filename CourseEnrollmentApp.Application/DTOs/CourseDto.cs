namespace CourseEnrollmentApp.Application.DTOs;

public class CourseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";

    public string Thumbnail { get; set; } = "";
    public string Category { get; set; } = "";

    public List<int> EnrolledStudentIds { get; set; } = new();
}
