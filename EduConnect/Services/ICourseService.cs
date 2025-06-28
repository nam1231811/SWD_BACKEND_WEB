using EduConnect.DTO;

namespace EduConnect.Services;

public interface ICourseService
{
    Task<string> CreateAsync(CourseCreate dto);
    Task<CourseCreate?> GetByIdAsync(string id);
}
