using EduConnect.Data;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace EduConnect.Services
{
    public class StudentStatusService
    {
        private readonly AppDbContext _context;

        public StudentStatusService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> GenerateFullStudentContextAsync(string parentId)
        {
            var students = await _context.Students
                .Include(s => s.Class)
                .Where(s => s.ParentId == parentId)
                .ToListAsync();

            if (!students.Any())
                return "Không tìm thấy học sinh nào thuộc phụ huynh này.";

            var sb = new StringBuilder();
            sb.AppendLine("Thông tin học sinh:");

            var today = DateTime.Today;
            var fullWeek = Enum.GetValues<DayOfWeek>();

            foreach (var student in students)
            {
                sb.AppendLine($"\n🧒 Học sinh: {student.FullName}");
                sb.AppendLine($"📛 Mã số: {student.StudentId}");
                sb.AppendLine($"🎂 Ngày sinh: {student.DateOfBirth?.ToString("dd/MM/yyyy") ?? "Không rõ"}");
                sb.AppendLine($"🏫 Lớp: {student.Class?.ClassName ?? "Không rõ"}");

                // ✅ Lịch học cả tuần
                sb.AppendLine("📚 Lịch học trong tuần & giáo viên:");
                foreach (var day in fullWeek)
                {
                    var courses = await _context.Courses
                         .Include(c => c.Teacher)
                         .ThenInclude(t => t.User)
                        .Where(c => c.ClassId == student.ClassId && c.DayOfWeek == day.ToString())
                        .ToListAsync();

                    if (courses.Any())
                    {
                        sb.AppendLine($"- {TranslateDay(day)}:");
                        foreach (var c in courses)
                        {
                            var start = c.StartTime?.ToString("HH:mm") ?? "?";
                            var end = c.EndTime?.ToString("HH:mm") ?? "?";
                            var subject = c.SubjectName;
                            var teacherName = c.Teacher?.User?.FullName ?? "Không rõ";
                            var teacherEmail = c.Teacher?.User?.Email ?? "Không rõ";
                            sb.AppendLine($"  + {subject} ({start} - {end}) - GV: {teacherName} ({teacherEmail})");
                        }
                    }
                }
                var homeroomTeacher = await _context.Classrooms
                .Include(c => c.Teacher)
                .ThenInclude(t => t.User)
                .Where(c => c.ClassId == student.ClassId)
                .Select(c => c.Teacher)
                .FirstOrDefaultAsync();

                if (homeroomTeacher?.User != null)
                {
                    sb.AppendLine("👨‍🏫 Giáo viên chủ nhiệm:");
                    sb.AppendLine($"- {homeroomTeacher.User.FullName} ({homeroomTeacher.User.Email})");
                }

                // ✅ Điểm
                var scores = await _context.Scores
                    .Include(s => s.Subject)
                    .Where(s => s.StudentId == student.StudentId)
                    .ToListAsync();

                if (scores.Any())
                {
                    sb.AppendLine("📊 Điểm số:");
                    foreach (var s in scores)
                    {
                        var subject = s.Subject?.SubjectName ?? "Không rõ";
                        sb.AppendLine($"- {subject}: {s.Score1}");
                    }
                }

                // ✅ Đi học hôm nay
                var dayName = today.DayOfWeek.ToString();
                var todayCourseIds = await _context.Courses
                    .Where(c => c.ClassId == student.ClassId && c.DayOfWeek == dayName)
                    .Select(c => c.CourseId)
                    .ToListAsync();

                var attendances = await _context.Attendances
                    .Where(a => a.StudentId == student.StudentId && todayCourseIds.Contains(a.CourseId))
                    .Include(a => a.Course)
                    .ToListAsync();

                if (attendances.Any())
                {
                    sb.AppendLine("📅 Đi học hôm nay:");
                    foreach (var a in attendances)
                    {
                        var subject = a.Course?.SubjectName ?? "Không rõ";
                        var status = a.Participation?.ToLower() == "có mặt" ? "Có mặt" : "Vắng";
                        sb.AppendLine($"- {subject}: {status}. Ghi chú: {a.Note}");
                    }
                }
            }

            sb.AppendLine("\nTrả lời mọi câu hỏi của phụ huynh dựa trên thông tin trên, bằng tiếng Việt, lịch sự, đúng ngữ cảnh.");
            return sb.ToString();
        }

        private string TranslateDay(DayOfWeek day)
        {
            return day switch
            {
                DayOfWeek.Monday => "Thứ hai",
                DayOfWeek.Tuesday => "Thứ ba",
                DayOfWeek.Wednesday => "Thứ tư",
                DayOfWeek.Thursday => "Thứ năm",
                DayOfWeek.Friday => "Thứ sáu",
                DayOfWeek.Saturday => "Thứ bảy",
                DayOfWeek.Sunday => "Chủ nhật",
                _ => day.ToString()
            };
        }
    }
}
