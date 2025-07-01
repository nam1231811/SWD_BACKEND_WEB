using EduConnect.Entities;

namespace EduConnect.Repositories
{
    public interface IReportRepository
    {
        Task<List<Report>> GetByClassIdAsync(string classId);
        Task<Report?> GetByIdAsync(string reportId);
        Task AddAsync(Report report);
        Task DeleteAsync(string reportId);
    }
}
