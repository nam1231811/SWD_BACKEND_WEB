using EduConnect.DTO;

namespace EduConnect.Services;

public interface ICourseService
{
    Task<string> CreateAsync(CourseCreate dto);
    Task<CourseCreate?> GetByIdAsync(string id);
    Task<List<CourseProfile>> GetByTeacherIdAsync(string teacherId);
    Task<List<CourseProfile>> GetByClassIdAsync(string classId);
    Task UpdateStatusAsync(UpdateCourse dto);
}
