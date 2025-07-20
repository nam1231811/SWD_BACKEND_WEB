using System.Text;
using EduConnect.Data;
using EduConnect.Entities;
using Microsoft.EntityFrameworkCore;

public class StudentQueryService
{
    private readonly AppDbContext _context;

    public StudentQueryService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<Student>> GetStudentsByClassIdAsync(string classId)
    {
        return await _context.Students
            .Where(s => s.ClassId == classId)
            .ToListAsync();
    }

    public async Task<string> GetStudentSummaryAsync(string studentId, Term term)
    {
        var student = await _context.Students
            .Include(s => s.Class)
            .FirstOrDefaultAsync(s => s.StudentId == studentId);

        if (student == null)
            return "Không tìm thấy học sinh.";

        var sb = new StringBuilder();

        sb.AppendLine($"📌 Báo cáo học sinh: {student.FullName}");
        sb.AppendLine($"🏫 Lớp: {student.Class?.ClassName ?? "Không rõ"}");

        // 📊 ĐIỂM SỐ
        var scores = await _context.Scores
            .Include(s => s.Subject)
            .Include(s => s.Semester)
            .Where(s =>
                s.StudentId == studentId &&
                s.Semester != null &&
                s.Semester.StartDate.HasValue &&
                s.Semester.EndDate.HasValue &&
                term.StartTime.HasValue &&
                term.EndTime.HasValue &&
                s.Semester.StartDate.Value.ToDateTime(TimeOnly.MinValue) >= term.StartTime.Value &&
                s.Semester.EndDate.Value.ToDateTime(TimeOnly.MaxValue) <= term.EndTime.Value
            )
            .ToListAsync();

        if (scores.Any())
        {
            sb.AppendLine("\n📊 Điểm số:");
            foreach (var s in scores)
            {
                var subject = s.Subject?.SubjectName ?? "Không rõ";
                sb.AppendLine($"- {subject}: {s.Score1}");
            }
        }
        else
        {
            sb.AppendLine("\n📊 Không có điểm nào trong kỳ.");
        }

        // ✅ ĐIỂM DANH THEO COURSE.STARTTIME
        var attendances = await _context.Attendances
            .Include(a => a.Course)
            .Where(a =>
                a.StudentId == studentId &&
                a.Course != null &&
                a.Course.StartTime.HasValue &&
                term.StartTime.HasValue &&
                term.EndTime.HasValue &&
                a.Course.StartTime.Value >= term.StartTime.Value &&
                a.Course.StartTime.Value <= term.EndTime.Value
            )
            .ToListAsync();

        if (attendances.Any())
        {
            var total = attendances.Count;
            var present = attendances.Count(a => a.Participation == "present");
            var absent = total - present;
            sb.AppendLine($"\n✅ Đi học: Có mặt {present}/{total} buổi. Vắng {absent} buổi.");
        }
        else
        {
            sb.AppendLine("\n✅ Không có dữ liệu điểm danh trong kỳ.");
        }

        return sb.ToString();
    }
}
