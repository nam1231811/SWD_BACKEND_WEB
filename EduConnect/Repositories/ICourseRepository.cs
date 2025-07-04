using EduConnect.Entities;

namespace EduConnect.Repositories;

public interface ICourseRepository
{
    Task<Course> AddAsync(Course course);
    Task<Course?> GetByIdAsync(string id);
    Task<List<Course>> GetByTeacherIdAsync(string teacherId);
    Task<List<Course>> GetByClassIdAsync(string classId);
}
