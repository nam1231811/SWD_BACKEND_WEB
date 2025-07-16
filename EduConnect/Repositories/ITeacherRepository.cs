using EduConnect.DTO;
using EduConnect.DTO.Teacher;
using EduConnect.Entities;

namespace EduConnect.Repositories
{
    public interface ITeacherRepository
    {
        Task<Teacher?> GetByUserIdAsync(string userId);
        Task<Teacher> CreateAsync(Teacher teacher);
        Task UpdateAsync(string userId, UpdateTeacher dto);
        Task DeleteAsync(string userId);
        Task UpdateFcmTokenAsync(string userId, string? fcmToken, string? platform);
        Task<TeacherFcmToken?> GetTeacherFcmByStudentIdAsync(string studentId);
    }
}
