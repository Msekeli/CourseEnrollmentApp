using CourseEnrollmentApp.Infrastructure.Data;
using CourseEnrollmentApp.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace CourseEnrollmentApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EnrollmentsController : ControllerBase
{
    private readonly AppDbContext _db;

    public EnrollmentsController(AppDbContext db)
    {
        _db = db;
    }

    // helper to extract logged-in student
    private int GetStudentId()
    {
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.Parse(id!);
    }

    // ⭐ ENROLL
    [HttpPost]
    public async Task<IActionResult> Enroll([FromBody] int courseId)
    {
        var studentId = GetStudentId();

        // Already enrolled?
        var exists = await _db.Enrollments
            .AnyAsync(e => e.StudentId == studentId && e.CourseId == courseId);

        if (exists)
            return BadRequest("Already enrolled");

        var enrollment = new Enrollment
        {
            StudentId = studentId,
            CourseId = courseId
        };

        _db.Enrollments.Add(enrollment);
        await _db.SaveChangesAsync();

        return Ok();
    }

    // GET MY COURSES
    [HttpGet("me")]
    public async Task<IActionResult> GetMyCourses()
    {
        var studentId = GetStudentId();

        var items = await _db.Enrollments
            .Where(e => e.StudentId == studentId)
            .Include(e => e.Course)
            .Select(e => new
            {
                e.CourseId,
                e.Course!.Title,
                e.Course.Category,
                e.Course.Thumbnail,
            })
            .ToListAsync();

        return Ok(items);
    }

    // UNENROLL
    [HttpDelete("{courseId}")]
    public async Task<IActionResult> Unenroll(int courseId)
    {
        var studentId = GetStudentId();

        var enrollment = await _db.Enrollments
            .FirstOrDefaultAsync(e => e.StudentId == studentId && e.CourseId == courseId);

        if (enrollment == null)
            return NotFound();

        _db.Enrollments.Remove(enrollment);
        await _db.SaveChangesAsync();

        return Ok();
    }
}
