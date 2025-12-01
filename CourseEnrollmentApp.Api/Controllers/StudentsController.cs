using CourseEnrollmentApp.Application.DTOs;
using CourseEnrollmentApp.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseEnrollmentApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    // GET ALL STUDENTS
    [HttpGet]
    public async Task<IActionResult> GetAllStudents()
    {
        var students = await _studentService.GetAllStudentsAsync();
        return Ok(students);
    }

    // GET STUDENT BY ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetStudentById(int id)
    {
        var student = await _studentService.GetStudentByIdAsync(id);
        if (student == null)
            return NotFound();

        return Ok(student);
    }

    // ENROLL STUDENT INTO A COURSE
    [HttpPost("{studentId}/enroll/{courseId}")]
    [Authorize]
    public async Task<IActionResult> EnrollStudent(int studentId, int courseId)
    {
        await _studentService.EnrollStudentAsync(studentId, courseId);
        return Ok("Enrolled successfully");
    }

    // UNENROLL
    [HttpPost("{studentId}/unenroll/{courseId}")]
    [Authorize]
    public async Task<IActionResult> UnenrollStudent(int studentId, int courseId)
    {
        await _studentService.UnenrollStudentAsync(studentId, courseId);
        return Ok("Unenrolled successfully");
    }

    // GET STUDENT COURSES
    [HttpGet("{studentId}/courses")]
    [Authorize]
    public async Task<IActionResult> GetStudentCourses(int studentId)
    {
        var courses = await _studentService.GetStudentCoursesAsync(studentId);
        return Ok(courses);
    }
}
