using EduConnect.DTO;
using EduConnect.Entities;

namespace EduConnect.Services
{
    public interface IReportService
    {
        Task<List<Report>> GetByClassIdAsync(string classId);
        Task<Report?> GetByIdAsync(string reportId);
        Task CreateAsync(ReportCreate dto);
        Task DeleteAsync(string reportId);
    }
}
