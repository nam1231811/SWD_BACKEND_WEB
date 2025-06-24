using EduConnect.Data;
using EduConnect.DTO;
using EduConnect.Entities;

namespace EduConnect.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly AppDbContext _appDbContext;

        public SubjectRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task CreateSubjectAsync(Subject dto)
        {
            _appDbContext.Subjects.Add(dto);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteSubjectAsync(string SubjectId)
        {
            var sub = await _appDbContext.Subjects.FindAsync(SubjectId);
            if (sub != null) 
            {
                _appDbContext.Subjects.Remove(sub);
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task<Subject> GetByIdAsync(string SubjectId)
        {
            return await _appDbContext.Subjects.FindAsync(SubjectId);
        }

        public async Task UpdateSubjectAsync(Subject dto)
        {
            _appDbContext.Subjects.Update(dto);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
