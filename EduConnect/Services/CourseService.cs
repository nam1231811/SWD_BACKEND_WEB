using EduConnect.DTO;
using EduConnect.Entities;
using EduConnect.Repositories;

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
            Status = dto.Status
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
            Status = course.Status
        };
    }
}
