using EduConnect.DTO;
using EduConnect.Entities;
using EduConnect.Utils;
using EduConnect.Data;
using EduConnect.Services;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Services;

public class ReportGenerationService
{
    private readonly StudentQueryService _studentQueryService;
    private readonly AppDbContext _context;

    public ReportGenerationService(StudentQueryService studentQueryService, AppDbContext context)
    {
        _studentQueryService = studentQueryService;
        _context = context;
    }

    public async Task<string> GenerateReportContentAsync(ReportBot input)
    {
        var term = await _context.Terms.FindAsync(input.TermId);
        var @class = await _context.Classrooms.FindAsync(input.ClassId);

        if (term == null || @class == null) return "Dữ liệu không hợp lệ.";

        var students = await _studentQueryService.GetStudentsByClassIdAsync(input.ClassId!);
        var studentIds = students.Select(s => s.StudentId).ToList();

        var scores = await _context.Scores
            .Include(s => s.Subject)
            .Include(s => s.Semester)
            .Where(s =>
                studentIds.Contains(s.StudentId) &&
                s.Semester != null &&
                s.Semester.StartDate.HasValue &&
                s.Semester.EndDate.HasValue
            )
            .ToListAsync(); // NGẮT QUERY Ở ĐÂY

        // Sau đó lọc ở phía C# (EF không xử lý nữa)
        scores = scores
            .Where(s =>
                s.Semester!.StartDate!.Value.ToDateTime(TimeOnly.MinValue) >= term.StartTime!.Value &&
                s.Semester.EndDate!.Value.ToDateTime(TimeOnly.MaxValue) <= term.EndTime!.Value
            )
            .ToList();

        var attendances = await _context.Attendances
            .Include(a => a.Course)
            .Where(a =>
                studentIds.Contains(a.StudentId) &&
                a.Course != null &&
                a.Course.StartTime.HasValue &&
                term.StartTime.HasValue &&
                term.EndTime.HasValue &&
                a.Course.StartTime.Value >= term.StartTime.Value &&
                a.Course.StartTime.Value <= term.EndTime.Value
            )
            .ToListAsync();

        var courses = await _context.Courses
            .Where(c =>
                c.ClassId == input.ClassId &&
                c.StartTime.HasValue &&
                term.StartTime.HasValue &&
                term.EndTime.HasValue &&
                c.StartTime.Value >= term.StartTime.Value &&
                c.StartTime.Value <= term.EndTime.Value
            )
            .ToListAsync();

        var builder = new ReportContentBuilder(students, scores, attendances, courses);
        return builder.BuildReport(term.Mode ?? "Monthly", @class.ClassName ?? "Lớp học");
    }
}
