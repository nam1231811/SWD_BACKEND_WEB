using EduConnect.DTO;
using EduConnect.DTO.Teacher;

namespace EduConnect.Services;

public interface ITeacherService
{
    Task<TeacherProfile?> GetByIdAsync(string id);
    Task<string> CreateAsync(CreateTeacher dto);
    Task<bool> UpdateAsync(string id, UpdateTeacher dto);
    Task<bool> DeleteAsync(string id);

    Task<string> AddScoreAsync(string teacherId, string subjectId, string studentId, decimal score);
    Task<bool> UpdateScoreAsync(string scoreId, decimal newScore);
}
