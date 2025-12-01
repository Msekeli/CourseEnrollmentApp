using System.Net.Http.Json;
using CourseEnrollmentApp.Application.DTOs;

namespace CourseEnrollmentApp.Client.Services;

public class CourseApiService
{
    private readonly HttpClient _http;

    public CourseApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<CourseDto>?> GetAllAsync()
    {
        return await _http.GetFromJsonAsync<List<CourseDto>>("api/courses");
    }

    public async Task<CourseDto?> GetByIdAsync(int id)
    {
        return await _http.GetFromJsonAsync<CourseDto>($"api/courses/{id}");
    }

    public async Task<CourseDto?> CreateAsync(CourseDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/courses", dto);
        return await response.Content.ReadFromJsonAsync<CourseDto>();
    }
}
