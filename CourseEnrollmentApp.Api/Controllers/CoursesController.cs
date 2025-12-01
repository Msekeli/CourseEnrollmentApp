using CourseEnrollmentApp.Application.DTOs;
using CourseEnrollmentApp.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseEnrollmentApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly ICourseService _courseService;

    public CoursesController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    // GET ALL COURSES
    [HttpGet]
    public async Task<IActionResult> GetAllCourses()
    {
        var courses = await _courseService.GetAllCoursesAsync();
        return Ok(courses);
    }

    // GET COURSE BY ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCourseById(int id)
    {
        var course = await _courseService.GetCourseByIdAsync(id);

        if (course == null)
            return NotFound();

        return Ok(course);
    }

    // CREATE A NEW COURSE
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateCourse(CourseDto dto)
    {
        var newCourse = await _courseService.CreateCourseAsync(dto);
        return Ok(newCourse);
    }
}
