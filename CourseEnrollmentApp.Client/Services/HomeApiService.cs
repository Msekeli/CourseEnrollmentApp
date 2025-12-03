using System.Net.Http.Json;
using CourseEnrollmentApp.Application.DTOs;

namespace CourseEnrollmentApp.Client.Services;

public class HomeApiService
{
    private readonly HttpClient _http;

    public HomeApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<CourseDto>> GetAllCoursesAsync()
    {
        return await _http.GetFromJsonAsync<List<CourseDto>>("api/courses") 
            ?? new List<CourseDto>();
    }

    public async Task<List<CourseDto>> GetMyCoursesAsync(int studentId)
    {
        return await _http.GetFromJsonAsync<List<CourseDto>>($"api/students/{studentId}/courses")
            ?? new List<CourseDto>();
    }
}
