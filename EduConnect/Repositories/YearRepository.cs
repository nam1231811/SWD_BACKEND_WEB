using EduConnect.Data;
using EduConnect.Entities;

namespace EduConnect.Repositories
{
    public class YearRepository : IYearRepository
    {
        private readonly AppDbContext _context;

        public YearRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<SchoolYear?> GetSchoolYearById(string id)
        {
            return await _context.SchoolYears.FindAsync(id);
        }
    }
}
