using CourseEnrollmentApp.Domain.Entities;

namespace CourseEnrollmentApp.Application.Interfaces;

public interface IStudentRepository
{
    Task<Student?> GetByIdAsync(int id);
    Task<List<Student>> GetAllAsync();
    Task AddAsync(Student student);
    Task UpdateAsync(Student student);
    Task DeleteAsync(int id);
}
