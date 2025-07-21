using System.Text;
using EduConnect.Entities;

namespace EduConnect.Utils;

public class ReportContentBuilder
{
    public string BuildReport(
        Term term,
        List<Classroom> classrooms,
        List<Student> students,
        List<Score> scores,
        List<Attendance> attendances,
        List<Course> courses)
    {
        var sb = new StringBuilder();

        // ===== TITLE =====
        sb.Append("📄 **BÁO CÁO HỌC TẬP (" + term.Mode + ")**\n\n");

        // ===== HEADER =====
        int totalStudents = students.Count;
        int totalCourses = courses.Count;
        int totalScores = scores.Count;
        int totalAttendances = attendances.Count;
        int totalAbsences = attendances.Count(a => a.Participation == "Vắng");

        sb.Append($"👨‍🎓 Tổng số học sinh: {totalStudents}\n");
        sb.Append($"📚 Số tiết học đã diễn ra: {totalCourses}\n");
        sb.Append($"📝 Số lượng điểm đã nhập: {totalScores}\n");
        sb.Append($"📅 Tổng lượt điểm danh: {totalAttendances}\n");
        sb.Append($"❌ Tổng số lượt vắng: {totalAbsences}\n\n");

        // ===== CHI TIẾT =====
        sb.Append("🔍 **CHI TIẾT**\n\n");

        foreach (var student in students)
        {
            var studentScores = scores
                .Where(s => s.StudentId == student.StudentId)
                .ToList();

            var studentAttendances = attendances
                .Where(a => a.StudentId == student.StudentId)
                .ToList();

            bool hasNote = studentAttendances.Any(a =>
                !string.IsNullOrEmpty(a.Note) ||
                !string.IsNullOrEmpty(a.Homework) ||
                !string.IsNullOrEmpty(a.Focus));

            if (hasNote)
            {
                sb.Append($"👦 Học sinh: {student.FullName}\n");

                foreach (var att in studentAttendances)
                {
                    string date = att.Course?.StartTime != null
                        ? att.Course.StartTime.Value.ToString("dd/MM/yyyy")
                        : "Không rõ";

                    string subject = att.Course?.SubjectName ?? "Chưa rõ môn";
                    string participation = att.Participation ?? "Không rõ";
                    string note = att.Note ?? "Không có";
                    string homework = att.Homework ?? "Không có";
                    string focus = att.Focus ?? "Không có";

                    sb.Append($"📅 {date} - {subject}\n");
                    sb.Append($"➡️ Tham gia: {participation} | 🎯 Tập trung: {focus}\n");
                    sb.Append($"📝 Ghi chú: {note}\n");
                    sb.Append($"📘 Bài tập: {homework}\n");
                    sb.Append("---\n");
                }

                sb.Append("\n"); // spacing giữa các học sinh
            }
        }

        return sb.ToString();
    }
}
