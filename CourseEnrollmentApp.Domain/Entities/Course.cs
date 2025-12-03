namespace CourseEnrollmentApp.Domain.Entities;

public class Course
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    // NEW UI-REQUIRED FIELDS
    public string Thumbnail { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    // Navigation
    public List<Enrollment> Enrollments { get; set; } = new();
}
