using EduConnect.DTO;
using EduConnect.DTO.Teacher;

namespace EduConnect.Services;

public interface ITeacherService
{
    Task<TeacherProfileDto?> GetByUserIdAsync(string userId);
    Task<TeacherProfileDto> CreateAsync(CreateTeacher dto);
    Task UpdateAsync(string userId, UpdateTeacher dto);
    Task DeleteAsync(string userId);

    Task UpdateFcmTokenAsync(string userId, UpdateFcmToken dto);
}
