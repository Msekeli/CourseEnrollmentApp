using CourseEnrollmentApp.Application.DTOs;

namespace CourseEnrollmentApp.Client.Services
{
    public class MyCoursesService
    {
        private readonly List<CourseDto> _myCourses = new();

        public IReadOnlyList<CourseDto> MyCourses => _myCourses;

        public void AddCourse(CourseDto c)
        {
            if (!_myCourses.Any(x => x.Id == c.Id))
                _myCourses.Add(c);
        }

        public void RemoveCourse(int id)
        {
            _myCourses.RemoveAll(x => x.Id == id);
        }
        public bool IsAdded(int id)
        {
            return _myCourses.Any(x => x.Id == id);
        }
    }
}
