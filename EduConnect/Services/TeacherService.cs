using EduConnect.DTO;
using EduConnect.DTO.Teacher;
using EduConnect.Entities;
using EduConnect.Repositories;

namespace EduConnect.Services;

public class TeacherService : ITeacherService
{
    private readonly ITeacherRepository _repo;
    public TeacherService(ITeacherRepository repo) => _repo = repo;

    public async Task<TeacherProfileDto?> GetByUserIdAsync(string userId)
    {
        var teacher = await _repo.GetByUserIdAsync(userId);
        if (teacher == null || teacher.User == null) return null;

        return new TeacherProfileDto
        {
            TeacherId = teacher.TeacherId,
            UserId = teacher.UserId!,
            SubjectId = teacher.SubjectId,
            Status = teacher.Status,
            FullName = $"{teacher.User.LastName} {teacher.User.FirstName}",
            FirstName = teacher.User.FirstName,
            LastName = teacher.User.LastName,
            Email = teacher.User.Email,
            PhoneNumber = teacher.User.PhoneNumber,
            CreateAt = teacher.User.CreateAt,
            UserImage = teacher.User.UserImage,
        };
    }

    public async Task<TeacherProfileDto> CreateAsync(CreateTeacher dto)
    {
        var teacher = new Teacher
        {
            TeacherId = Guid.NewGuid().ToString(),
            UserId = dto.UserId,
            SubjectId = dto.SubjectId,
            Status = dto.Status
        };

        var created = await _repo.CreateAsync(teacher);
        return await GetByUserIdAsync(created.UserId!) ?? throw new Exception("Cannot retrieve created teacher");
    }
    public async Task UpdateAsync(string userId, UpdateTeacher dto)
    {
        await _repo.UpdateAsync(userId, dto);
    }

    public async Task DeleteAsync(string userId)
    {
        await _repo.DeleteAsync(userId);
    }

    public async Task UpdateFcmTokenAsync(string userId, UpdateFcmToken dto)
    {
        await _repo.UpdateFcmTokenAsync(userId, dto.FcmToken, dto.Platform);
    }

    public async Task<ParentProfile?> GetParentProfileByStudentIdAsync(string studentId)
    {
        return await _repo.GetParentProfileByStudentIdAsync(studentId);
    }
}


