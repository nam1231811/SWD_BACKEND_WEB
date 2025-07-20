using EduConnect.Entities;
using System.Text;

namespace EduConnect.Utils;

public class ReportContentBuilder
{
    private readonly List<Student> _students;
    private readonly List<Score> _scores;
    private readonly List<Attendance> _attendances;
    private readonly List<Course> _courses;

    public ReportContentBuilder(
        List<Student> students,
        List<Score> scores,
        List<Attendance> attendances,
        List<Course> courses)
    {
        _students = students;
        _scores = scores;
        _attendances = attendances;
        _courses = courses;
    }

    public string BuildReport(string mode, string className)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"📘 Báo cáo học tập ({mode}) của lớp {className}");
        sb.AppendLine($"Tổng số học sinh: {_students.Count}");

        sb.AppendLine($"\n📚 Số tiết học đã diễn ra: {_courses.Count}");

        var totalScoreCount = _scores.Count;
        sb.AppendLine($"\n📊 Số lượng điểm đã nhập: {totalScoreCount}");

        var totalAttendance = _attendances.Count;
        var totalAbsent = _attendances.Count(a => a.Participation != "present");
        sb.AppendLine($"\n📝 Tổng lượt điểm danh: {totalAttendance}");
        sb.AppendLine($"🚫 Tổng số lượt vắng: {totalAbsent}");

        sb.AppendLine("\n👩‍🎓 Thông tin chi tiết học sinh:");

        // Lặp qua từng học sinh
        foreach (var student in _students)
        {
            sb.AppendLine($"\n🔹 Mã học sinh: {student.StudentId}");
            sb.AppendLine($"👤 Họ tên: {student.FullName}");

            // Tìm các attendance của học sinh này
            var studentAttendances = _attendances.Where(a => a.StudentId == student.StudentId).ToList();

            if (studentAttendances.Count == 0)
            {
                sb.AppendLine("📌 Không có dữ liệu điểm danh.");
            }
            else
            {
                foreach (var att in studentAttendances)
                {
                    var courseName = att.Course?.SubjectName ?? "Chưa rõ môn";
                    sb.AppendLine($"📅 Môn: {courseName}");
                    sb.AppendLine($"📍 Tham gia: {att.Participation}");
                    sb.AppendLine($"📝 Ghi chú: {att.Note}");
                    sb.AppendLine($"📚 Bài tập: {att.Homework}");
                    sb.AppendLine($"🎯 Tập trung: {att.Focus}");
                    sb.AppendLine("---");
                }
            }
        }
        return sb.ToString();
    }
}
