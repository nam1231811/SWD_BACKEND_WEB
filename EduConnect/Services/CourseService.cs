using EduConnect.DTO;
using EduConnect.Entities;
using EduConnect.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _repo;

    public CourseService(ICourseRepository repo)
    {
        _repo = repo;
    }

    public async Task<string> CreateAsync(CourseCreate dto)
    {
        var course = new Course
        {
            CourseId = Guid.NewGuid().ToString(),
            ClassId = dto.ClassId,
            TeacherId = dto.TeacherId,
            SemeId = dto.SemeId,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            DayOfWeek = dto.DayOfWeek,
            Status = dto.Status,
            SubjectName = dto.SubjectName
        };

        var result = await _repo.AddAsync(course);
        return result.CourseId;
    }

    public async Task<CourseCreate?> GetByIdAsync(string id)
    {
        var course = await _repo.GetByIdAsync(id);
        if (course == null) return null;

        return new CourseCreate
        {
            ClassId = course.ClassId,
            TeacherId = course.TeacherId,
            SemeId = course.SemeId,
            StartTime = course.StartTime,
            EndTime = course.EndTime,
            DayOfWeek = course.DayOfWeek,
            Status = course.Status,
            SubjectName = course.SubjectName
        };
    }

    public async Task<List<CourseProfile>> GetByTeacherIdAsync(string teacherId)
    {
        var courses = await _repo.GetByTeacherIdAsync(teacherId);

        return courses.Select(c => new CourseProfile
        {
            CourseId = c.CourseId,
            ClassId = c.ClassId,
            TeacherId = c.TeacherId,
            SemeId = c.SemeId,
            StartTime = c.StartTime,
            EndTime = c.EndTime,
            DayOfWeek = c.DayOfWeek,
            Status = c.Status,
            SubjectName = c.SubjectName
        }).ToList();
    }

    public async Task<List<CourseProfile>> GetByClassIdAsync(string classId)
    {
        var courses = await _repo.GetByClassIdAsync(classId);

        return courses.Select(c => new CourseProfile
        {
            CourseId = c.CourseId,
            ClassId = c.ClassId,
            TeacherId = c.TeacherId,
            SemeId = c.SemeId,
            StartTime = c.StartTime,
            EndTime = c.EndTime,
            DayOfWeek = c.DayOfWeek,
            Status = c.Status,
            SubjectName = c.SubjectName
        }).ToList();
    }
    public async Task UpdateStatusAsync(UpdateCourse dto)
    {
        await _repo.UpdateStatusAsync(dto.CourseId, dto.Status);
    }
}
