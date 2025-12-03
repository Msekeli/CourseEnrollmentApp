using CourseEnrollmentApp.Application.Interfaces;
using CourseEnrollmentApp.Domain.Entities;
using CourseEnrollmentApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseEnrollmentApp.Infrastructure.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly AppDbContext _context;

    public CourseRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Course?> GetByIdAsync(int id)
    {
        return await _context.Courses
            .Include(c => c.Enrollments)
            .ThenInclude(e => e.Student)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Course>> GetAllAsync()
    {
        return await _context.Courses
            .Include(c => c.Enrollments)
            .ThenInclude(e => e.Student)
            .ToListAsync();
    }

    public async Task AddAsync(Course course)
    {
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Course course)
    {
        _context.Courses.Update(course);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var course = await GetByIdAsync(id);
        if (course != null)
        {
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }
    }
}
