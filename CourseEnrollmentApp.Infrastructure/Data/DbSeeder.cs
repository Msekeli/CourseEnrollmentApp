using CourseEnrollmentApp.Domain.Entities;

namespace CourseEnrollmentApp.Infrastructure.Data;

public static class DbSeeder
{
    public static void Seed(AppDbContext context)
    {
        if (context.Courses.Any()) return;

        var students = new List<Student>
        {
            new Student { Id = 1, Name = "John Doe", Email = "john@example.com" },
            new Student { Id = 2, Name = "Sarah Moyo", Email = "sarah@example.com" },
            new Student { Id = 3, Name = "Litha Nkosi", Email = "litha@example.com" },
            new Student { Id = 4, Name = "Nolwazi Hadebe", Email = "nolwazi@example.com" },
            new Student { Id = 5, Name = "Msekeli Mkwibiso", Email = "msekeli@example.com" }
        };

        var courses = new List<Course>
        {
            new Course {
                Id = 1,
                Title = "Mastering C#",
                Category = "Programming",
                Thumbnail = "https://images.unsplash.com/photo-1587620962725-abab7fe55159?q=80&w=600",
                Description = "Learn C# from fundamentals to advanced techniques."
            },
            new Course {
                Id = 2,
                Title = "UI/UX Essentials",
                Category = "Design",
                Thumbnail = "https://images.unsplash.com/photo-1559028012-481c04fa702d?q=80&w=600",
                Description = "Explore modern design principles and user-centered design."
            },
            new Course {
                Id = 3,
                Title = "JavaScript 101",
                Category = "Frontend",
                Thumbnail = "https://images.unsplash.com/photo-1515879218367-8466d910aaa4?q=80&w=600",
                Description = "Begin your web development journey with JavaScript basics."
            },
            new Course {
                Id = 4,
                Title = "Docker Fundamentals",
                Category = "DevOps",
                Thumbnail = "https://images.unsplash.com/photo-1605902711622-cfb43c44367e?q=80&w=600",
                Description = "Understand containerization and DevOps workflows."
            },
            new Course {
                Id = 5,
                Title = "SQL Basics",
                Category = "Database",
                Thumbnail = "https://images.unsplash.com/photo-1519389950473-47ba0277781c?q=80&w=600",
                Description = "Master SQL queries, joins, indexes, and relational design."
            },
            new Course {
                Id = 6,
                Title = "Azure Cloud Essentials",
                Category = "Cloud",
                Thumbnail = "https://images.unsplash.com/photo-1521791055366-0d553872125f?q=80&w=600",
                Description = "Learn cloud concepts and Azure core services."
            },
            new Course {
                Id = 7,
                Title = "API Design Basics",
                Category = "Backend",
                Thumbnail = "https://images.unsplash.com/photo-1581091012184-5c9af70e14b6?q=80&w=600",
                Description = "Build clean and scalable REST APIs."
            },
            new Course {
                Id = 8,
                Title = "React From Scratch",
                Category = "Frontend",
                Thumbnail = "https://images.unsplash.com/photo-1526378726877-0ee4e8c5d06b?q=80&w=600",
                Description = "Practical React.js for building interactive UIs."
            },
            new Course {
                Id = 9,
                Title = "Unit Testing with xUnit",
                Category = "Testing",
                Thumbnail = "https://images.unsplash.com/photo-1584697964154-3e5a61c05a2d?q=80&w=600",
                Description = "Write reliable tests for .NET applications."
            },
            new Course {
                Id = 10,
                Title = "DevOps & CI/CD",
                Category = "DevOps",
                Thumbnail = "https://images.unsplash.com/photo-1581091215367-1c5c1b7b6a8b?q=80&w=600",
                Description = "Automate builds and deployments with CI/CD pipelines."
            }
        };

        var enrollments = new List<Enrollment>
        {
            new Enrollment { StudentId = 1, CourseId = 1 },
            new Enrollment { StudentId = 1, CourseId = 5 },
            new Enrollment { StudentId = 1, CourseId = 6 },

            new Enrollment { StudentId = 2, CourseId = 2 },
            new Enrollment { StudentId = 2, CourseId = 3 },
            new Enrollment { StudentId = 2, CourseId = 9 },

            new Enrollment { StudentId = 3, CourseId = 4 },
            new Enrollment { StudentId = 3, CourseId = 8 },

            new Enrollment { StudentId = 4, CourseId = 1 },
            new Enrollment { StudentId = 4, CourseId = 2 },
            new Enrollment { StudentId = 4, CourseId = 3 },
            new Enrollment { StudentId = 4, CourseId = 10 },

            new Enrollment { StudentId = 5, CourseId = 7 },
            new Enrollment { StudentId = 5, CourseId = 8 }
        };

        context.Students.AddRange(students);
        context.Courses.AddRange(courses);
        context.Enrollments.AddRange(enrollments);
        context.SaveChanges();
    }
}
