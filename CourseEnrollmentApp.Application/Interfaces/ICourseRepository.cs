using CourseEnrollmentApp.Domain.Entities;

namespace CourseEnrollmentApp.Application.Interfaces;

public interface ICourseRepository
{
    Task<Course?> GetByIdAsync(int id);
    Task<List<Course>> GetAllAsync();
    Task AddAsync(Course course);
    Task UpdateAsync(Course course);
    Task DeleteAsync(int id);
}
