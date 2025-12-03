# CourseEnrollmentApp

A Blazor WebAssembly application with an ASP.NET Web API backend using **Clean Architecture**.

## Features
- Register & login with JWT  
- View available courses  
- Enroll & unenroll  
- View enrolled courses  
- EF Core InMemory database  

## Architecture
- **Domain** – Entities  
- **Application** – DTOs, interfaces, services  
- **Infrastructure** – Repositories, EF Core, JWT  
- **API** – Auth, Courses, Enrollments  
- **Client** – Blazor WASM UI

## Run

### Backend
```bash
cd CourseEnrollmentApp.Api
dotnet watch run
### Frontend
```bash
cd CourseEnrollmentApp.client
dotnet watch run
