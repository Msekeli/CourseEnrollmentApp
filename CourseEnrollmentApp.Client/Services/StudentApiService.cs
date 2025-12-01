using System.Net.Http.Json;
using CourseEnrollmentApp.Application.DTOs;

namespace CourseEnrollmentApp.Client.Services;
public class StudentApiService
{
    private readonly HttpClient _http;

    public StudentApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<StudentDto>?> GetAllAsync()
    {
        return await _http.GetFromJsonAsync<List<StudentDto>>("api/students");
    }

    public async Task<StudentDto?> GetByIdAsync(int id)
    {
        return await _http.GetFromJsonAsync<StudentDto>($"api/students/{id}");
    }

    public async Task<bool> EnrollAsync(int studentId, int courseId)
    {
        var response = await _http.PostAsync($"api/students/{studentId}/enroll/{courseId}", null);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UnenrollAsync(int studentId, int courseId)
    {
        var response = await _http.PostAsync($"api/students/{studentId}/unenroll/{courseId}", null);
        return response.IsSuccessStatusCode;
    }

    public async Task<List<CourseDto>?> GetStudentCoursesAsync(int studentId)
    {
        return await _http.GetFromJsonAsync<List<CourseDto>>($"api/students/{studentId}/courses");
    }
}
