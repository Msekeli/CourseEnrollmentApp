namespace CourseEnrollmentApp.Domain.Entities;

public class Student
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    // Navigation
    public List<Enrollment> Enrollments { get; set; } = new();
}
