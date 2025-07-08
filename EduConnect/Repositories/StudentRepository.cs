using EduConnect.Data;
using EduConnect.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;

        public StudentRepository (AppDbContext context)
        {
            _context = context;
        }

        public async Task<Student?> GetByIdAsync(string studentId)
        {
            return await _context.Students.FirstOrDefaultAsync(s => s.StudentId == studentId);
        }

        public async Task UpdateAsync(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Student>> GetByClassIdAsync(string classId)
        {
            return await _context.Students
                .Where(s => s.ClassId == classId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await _context.Students
            .Include(s => s.Class)
            .Include(s => s.Parent)
            .ToListAsync();
        }
    }
}
