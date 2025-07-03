using EduConnect.Data;
using EduConnect.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly AppDbContext _context;

    public CourseRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Course> AddAsync(Course course)
    {
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();
        return course;
    }

    public async Task<Course?> GetByIdAsync(string id)
    {
        return await _context.Courses
            .Include(c => c.Class)
            .Include(c => c.Teacher)
            .Include(c => c.Semester)
            .FirstOrDefaultAsync(c => c.CourseId == id);
    }

    public async Task<List<Course>> GetByTeacherIdAsync(string teacherId)
    {
        return await _context.Courses
            .Where(c => c.TeacherId == teacherId)
            .ToListAsync();
    }

}
