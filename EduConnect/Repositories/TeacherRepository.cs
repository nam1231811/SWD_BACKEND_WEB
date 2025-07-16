using EduConnect.Data;
using EduConnect.DTO;
using EduConnect.DTO.Teacher;
using EduConnect.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly AppDbContext _context;
        public TeacherRepository(AppDbContext context) => _context = context;

        public async Task<Teacher?> GetByUserIdAsync(string userId)
        {
            return await _context.Teachers
                .Include(t => t.User)
                .Include(t => t.Subject)
                .Include(t => t.Classroom)
                .FirstOrDefaultAsync(t => t.UserId == userId);
        }

        public async Task<Teacher> CreateAsync(Teacher teacher)
        {
            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();
            return teacher;
        }

        public async Task UpdateAsync(string userId, UpdateTeacher dto)
        {
            var teacher = await _context.Teachers.FirstOrDefaultAsync(t => t.UserId == userId);
            if (teacher != null)
            {
                teacher.SubjectId = dto.SubjectId ?? teacher.SubjectId;
                teacher.Status = dto.Status ?? teacher.Status;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(string userId)
        {
            var teacher = await _context.Teachers.FirstOrDefaultAsync(t => t.UserId == userId);
            if (teacher != null)
            {
                _context.Teachers.Remove(teacher);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateFcmTokenAsync(string userId, string? fcmToken, string? platform)
        {
            var teacher = await _context.Teachers.FirstOrDefaultAsync(t => t.UserId == userId);
            if (teacher != null)
            {
                teacher.FcmToken = fcmToken;
                teacher.Platform = platform;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<TeacherFcmToken?> GetTeacherFcmByStudentIdAsync(string studentId)
        {
            return await _context.Students
            .Where(s => s.StudentId == studentId)
            .Include(s => s.Class)
                .ThenInclude(c => c.Teacher)
            .Select(s => new TeacherFcmToken
            {
                TeacherId = s.Class.Teacher.TeacherId,
                FcmToken = s.Class.Teacher.FcmToken,
                Platform = s.Class.Teacher.Platform
            })
            .FirstOrDefaultAsync();
        }
    }

}
