using System.Text;
using System.Text.RegularExpressions;
using EduConnect.Data;
using EduConnect.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Services
{
    public class QuestionAnalysisService
    {
        private readonly StudentStatusService _statusService;
        private readonly AppDbContext _context;

        public QuestionAnalysisService(StudentStatusService statusService, AppDbContext context)
        {
            _statusService = statusService;
            _context = context;
        }

        public async Task<string> BuildRelevantPromptAsync(string parentId, string userMessage)
        {
            var lower = userMessage.ToLower();
            var sb = new StringBuilder();

            sb.AppendLine($"Câu hỏi từ phụ huynh: \"{userMessage}\"\n");

            var students = await _context.Students
                .Include(s => s.Class)
                .ThenInclude(c => c.Teacher).ThenInclude(t => t.User)
                .Where(s => s.ParentId == parentId)
                .ToListAsync();

            if (!students.Any())
            {
                sb.AppendLine("❌ Không tìm thấy học sinh nào cho phụ huynh này.");
                return sb.ToString();
            }

            var student = students.First(); // Ưu tiên 1 học sinh nếu có nhiều

            if (Regex.IsMatch(lower, @"(con tôi|thông tin học sinh|học sinh của tôi|con của tôi|con|thông tin cá nhân của con)"))
            {
                var info = await _statusService.GetBasicStudentInfoAsync(parentId);
                sb.AppendLine(info);
            }
            else if (Regex.IsMatch(lower, @"(thông tin cá nhân|tôi là ai|thông tin phụ huynh)"))
            {
                var info = await _statusService.GetParentInfoAsync(parentId);
                sb.AppendLine(info);
            }
            else if (Regex.IsMatch(lower, @"(học gì hôm nay|hôm nay học gì|lịch học hôm nay)"))
            {
                var today = await _statusService.GetTodayLearningStatusAsync(student);
                sb.AppendLine(today);
            }
            else if (Regex.IsMatch(lower, @"(tình hình học tập hôm nay|học hành hôm nay thế nào|hôm nay học ra sao)"))
            {
                var summary = await _statusService.GetTodayStatusSummaryAsync(student);
                sb.AppendLine(summary);
            }
            else if (Regex.IsMatch(lower, @"(lịch học ngày mai|lịch học tuần sau|học gì ngày mai)"))
            {
                var upcoming = await _statusService.GetUpcomingScheduleAsync(student);
                sb.AppendLine(upcoming);
            }
            else if (Regex.IsMatch(lower, @"(lịch học|thời khóa biểu)"))
            {
                var schedule = await _statusService.GetWeeklyScheduleAsync(student.ClassId);
                sb.AppendLine(schedule);
            }
            else if (Regex.IsMatch(lower, @"(điểm|kết quả học tập)"))
            {
                var scores = await _statusService.GetStudentScoresAsync(student.StudentId);
                sb.AppendLine(scores);
            }
            else if (Regex.IsMatch(lower, @"(giáo viên chủ nhiệm|gvcn)"))
            {
                var basicInfo = await _statusService.GetBasicStudentInfoAsync(parentId);
                sb.AppendLine(basicInfo);
            }
            else
            {
                // fallback toàn bộ context
                string context = await _statusService.GenerateStudentContextByTypeAsync(parentId, "schedule"); // hoặc today, schedule...
                sb.AppendLine("📎 Ngữ cảnh đầy đủ:");
                sb.AppendLine(context);
            }

            return sb.ToString();
        }
    }
}
