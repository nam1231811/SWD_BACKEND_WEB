using EduConnect.DTO;
using EduConnect.Entities;
using EduConnect.Repositories;

namespace EduConnect.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;

        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task CreateAsync(ReportCreate dto)
        {
            var report = new Report
            {
                ReportId = Guid.NewGuid().ToString(),
                TeacherId = dto.TeacherId,
                Title = dto.Title,
                Description = dto.Description,
                ClassId = dto.ClassId,
                ClassName = dto.ClassName,
                TeacherName = dto.TeacherName,
                TermId = dto.TermID,
            };
            await _reportRepository.AddAsync(report);
        }

        public async Task DeleteAsync(string reportId)
        {
            await _reportRepository.DeleteAsync(reportId);
        }

        //tim toan bo report trong class
        public async Task<List<Report>> GetByClassIdAsync(string classId)
        {
            var list = await _reportRepository.GetByClassIdAsync(classId);
            return list.Select(n => new Report
            {
                ReportId = n.ReportId,
                Title = n.Title,
                Description = n.Description,
                ClassId = n.ClassId,
                TeacherId = n.TeacherId,
                TermId = n.TermId,
                TeacherName= n.TeacherName,
                ClassName = n.ClassName,
            }).ToList();
        }

        //tim report by id
        public async Task<Report?> GetByIdAsync(string reportId)
        {
            var n = await _reportRepository.GetByIdAsync(reportId);
            if (n == null) return null;
            return new Report
            {
                ReportId = n.ReportId,
                Title = n.Title,
                Description = n.Description,
                ClassId = n.ClassId,
                TeacherId = n.TeacherId,
                TermId = n.TermId,
                ClassName = n.ClassName,
                TeacherName = n.TeacherName,
            };
        }
    }
}
