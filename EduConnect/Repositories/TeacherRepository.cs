using EduConnect.Data;
using EduConnect.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly AppDbContext _context;

        public TeacherRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Teacher?> GetByIdAsync(string id)
        {
            return await _context.Teachers
                .Include(t => t.User)
                .Include(t => t.Subject)
                .Include(t => t.Classroom)
                .FirstOrDefaultAsync(t => t.TeacherId == id);
        }

        public async Task<Teacher> AddAsync(Teacher teacher)
        {
            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();
            return teacher;
        }

        public async Task UpdateAsync(Teacher teacher)
        {
            _context.Teachers.Update(teacher);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Teacher teacher)
        {
            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
        }

        public async Task<Score> AddScoreAsync(Score score)
        {
            _context.Scores.Add(score);
            await _context.SaveChangesAsync();
            return score;
        }

        public async Task<bool> UpdateScoreAsync(string scoreId, decimal newScore)
        {
            var score = await _context.Scores.FindAsync(scoreId);
            if (score == null) return false;

            score.Score1 = newScore;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
