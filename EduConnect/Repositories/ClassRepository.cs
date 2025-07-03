using EduConnect.Data;
using EduConnect.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly AppDbContext _appDbContext;

        public ClassRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task CreateClassAsync(Classroom dto)
        {
            _appDbContext.Classrooms.Add(dto);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteClassAsync(string ClassId)
        {
            var classroom = await _appDbContext.Classrooms.FindAsync(ClassId);
            if (classroom != null)
            {
                _appDbContext.Classrooms.Remove(classroom);
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task<Classroom> GetByIdAsync(string ClassId)
        {
            return await _appDbContext.Classrooms.FindAsync(ClassId);
        }

        public async Task UpdateClassAsync(Classroom dto)
        {
            _appDbContext.Classrooms.Update(dto);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<Classroom>> GetByTeacherIdAsync(string teacherId)
        {
            return await _appDbContext.Classrooms
                .Where(c => c.TeacherId == teacherId)
                .ToListAsync();
        }
    }
}
