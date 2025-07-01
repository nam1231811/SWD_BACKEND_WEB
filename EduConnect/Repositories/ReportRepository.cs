using EduConnect.Data;
using EduConnect.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly AppDbContext _appDbContext;

        public ReportRepository (AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(Report report)
        {
            _appDbContext.Reports.Add(report);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(string reportId)
        {
            var report = await _appDbContext.Reports.FindAsync(reportId);
            if (report != null)
            {
                _appDbContext.Reports.Remove(report);
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Report>> GetByClassIdAsync(string classId)
        {
            return await _appDbContext.Reports
            .Include(n => n.Class)
            .Include(n => n.Teacher)
            .Where(n => n.ClassId == classId)
            .ToListAsync();
        }

        public async Task<Report?> GetByIdAsync(string reportId)
        {
            return await _appDbContext.Reports
            .Include(n => n.Class)
            .Include(n => n.Teacher)
            .FirstOrDefaultAsync(n => n.ReportId == reportId);
        }
    }
}
