using EduConnect.DTO;
using EduConnect.DTO.Teacher;
using EduConnect.Entities;
using EduConnect.Repositories;

namespace EduConnect.Services;

public class TeacherService : ITeacherService
{
    private readonly ITeacherRepository _repo;

    public TeacherService(ITeacherRepository repo)
    {
        _repo = repo;
    }

    public async Task<TeacherProfile?> GetByIdAsync(string id)
    {
        var teacher = await _repo.GetByIdAsync(id);
        if (teacher == null) return null;

        return new TeacherProfile
        {
            TeacherId = teacher.TeacherId,
            UserId = teacher.UserId,
            SubjectId = teacher.SubjectId,
            Status = teacher.Status
        };
    }

    public async Task<string> CreateAsync(CreateTeacher dto)
    {
        var teacher = new Teacher
        {
            TeacherId = Guid.NewGuid().ToString(),
            UserId = dto.UserId,
            SubjectId = dto.SubjectId,
            Status = dto.Status
        };

        var result = await _repo.AddAsync(teacher);
        return result.TeacherId;
    }

    public async Task<bool> UpdateAsync(string id, UpdateTeacher dto)
    {
        var teacher = await _repo.GetByIdAsync(id);
        if (teacher == null) return false;

        teacher.SubjectId = dto.SubjectId;
        teacher.Status = dto.Status;

        await _repo.UpdateAsync(teacher);
        return true;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var teacher = await _repo.GetByIdAsync(id);
        if (teacher == null) return false;

        await _repo.DeleteAsync(teacher);
        return true;
    }

    public async Task<string> AddScoreAsync(string teacherId, string subjectId, string studentId, decimal score)
    {
        var newScore = new Score
        {
            ScoreId = Guid.NewGuid().ToString(),
            SubjectId = subjectId,
            StudentId = studentId,
            Score1 = score
        };

        var result = await _repo.AddScoreAsync(newScore);
        return result.ScoreId;
    }

    public async Task<bool> UpdateScoreAsync(string scoreId, decimal newScore)
    {
        return await _repo.UpdateScoreAsync(scoreId, newScore);
    }
}
