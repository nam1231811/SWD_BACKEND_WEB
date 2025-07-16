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
            var sb = new StringBuilder();

            // 👨‍👩‍👧‍👦 Thông tin phụ huynh
            var parent = await _context.Parents
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.ParentId == parentId);

            if (parent != null)
            {
                sb.AppendLine("👨‍👩‍👧‍👦 Thông tin phụ huynh:");
                sb.AppendLine($"- Họ tên: {parent.User?.FullName ?? "Không rõ"}");
                sb.AppendLine($"- Email: {parent.User?.Email ?? "Không rõ"}");
                sb.AppendLine($"- Số điện thoại: {parent.User?.PhoneNumber ?? "Không rõ"}\n");
            }
            else
            {
                sb.AppendLine("⚠️ Không tìm thấy thông tin phụ huynh.\n");
            }

            // 🧒 Học sinh
            var students = await _context.Students
                .Include(s => s.Class)
                .Where(s => s.ParentId == parentId)
                .ToListAsync();

            if (!students.Any())
                return sb.AppendLine("❌ Không tìm thấy học sinh nào thuộc phụ huynh này.").ToString();

            var today = DateTime.Today;
            var daysToCheck = new[] { today, today.AddDays(1), today.AddDays(7) };

            foreach (var student in students)
            {
                sb.AppendLine("=========================================");
                sb.AppendLine("🧒 Thông tin học sinh:");
                sb.AppendLine($"- Họ tên: {student.FullName}");
                sb.AppendLine($"- Mã số: {student.StudentId}");
                sb.AppendLine($"- Ngày sinh: {student.DateOfBirth?.ToString("dd/MM/yyyy") ?? "Không rõ"}");
                sb.AppendLine($"- Lớp: {student.Class?.ClassName ?? "Không rõ"}");

                // 📚 THỜI KHÓA BIỂU
                sb.AppendLine("\n📚 Thời khóa biểu trong tuần:");
                foreach (var day in Enum.GetValues<DayOfWeek>())
                {
                    var vnDay = TranslateDay(day);

                    var courses = await _context.Courses
                        .Include(c => c.Teacher)
                        .ThenInclude(t => t.User)
                        .Where(c => c.ClassId == student.ClassId && c.DayOfWeek == vnDay)
                        .OrderBy(c => c.StartTime)
                        .ToListAsync();

                    if (courses.Any())
                    {
                        sb.AppendLine($"- {vnDay}:");
                        foreach (var c in courses)
                        {
                            var subject = c.SubjectName;
                            var start = c.StartTime?.ToString("HH:mm") ?? "?";
                            var end = c.EndTime?.ToString("HH:mm") ?? "?";
                            var teacher = c.Teacher?.User?.FullName ?? "Không rõ";

                            sb.AppendLine($"  + {subject} ({start} - {end}) - GV: {teacher}");
                        }
                    }
                }

                // 👨‍🏫 GVCN
                var homeroom = await _context.Classrooms
                    .Include(c => c.Teacher)
                    .ThenInclude(t => t.User)
                    .Where(c => c.ClassId == student.ClassId)
                    .Select(c => c.Teacher)
                    .FirstOrDefaultAsync();

                if (homeroom?.User != null)
                {
                    sb.AppendLine("\n👨‍🏫 Giáo viên chủ nhiệm:");
                    sb.AppendLine($"- {homeroom.User.FullName} ({homeroom.User.Email})");
                }

                // 📊 ĐIỂM SỐ THEO HỌC KỲ & LOẠI
                var scores = await _context.Scores
                    .Include(s => s.Subject)
                    .Include(s => s.Semester)
                    .Where(s => s.StudentId == student.StudentId)
                    .ToListAsync();

                if (scores.Any())
                {
                    sb.AppendLine("\n📊 Điểm số theo từng học kỳ và loại điểm:");

                    // Nhóm theo học kỳ
                    var scoresBySemester = scores
                        .GroupBy(s => s.Semester?.SemesterName ?? "Không rõ học kỳ");

                    foreach (var semGroup in scoresBySemester)
                    {
                        sb.AppendLine($"\n📘 Học kỳ: {semGroup.Key}");

                        // Nhóm theo môn học
                        var subjectGroups = semGroup
                            .GroupBy(s => s.Subject?.SubjectName ?? "Không rõ môn");

                        foreach (var subjGroup in subjectGroups)
                        {
                            sb.AppendLine($"- Môn: {subjGroup.Key}");

                            // Nhóm theo loại điểm (15p, giữa kỳ, cuối kỳ, v.v.)
                            var typeGroups = subjGroup
                                .GroupBy(s => s.Type ?? "Không rõ loại điểm");

                            foreach (var typeGroup in typeGroups)
                            {
                                var type = typeGroup.Key;
                                var scoresText = string.Join(", ", typeGroup.Select(s => s.Score1?.ToString("0.##") ?? "?"));

                                sb.AppendLine($"  + {type}: {scoresText}");
                            }
                        }
                    }
                }

                // 📅 HỌC GÌ HÔM NAY
                sb.AppendLine("\n📅 Tình hình học tập hôm nay:");
                var todayVN = TranslateDay(today.DayOfWeek);

                var todayCourses = await _context.Courses
                    .Where(c => c.ClassId == student.ClassId && c.DayOfWeek == todayVN)
                    .ToListAsync();

                var attendanceToday = await _context.Attendances
                    .Include(a => a.Course)
                    .Where(a => a.StudentId == student.StudentId && todayCourses.Select(c => c.CourseId).Contains(a.CourseId))
                    .ToListAsync();

                if (attendanceToday.Any())
                {
                    foreach (var a in attendanceToday)
                    {
                        var subject = a.Course?.SubjectName ?? "Không rõ";
                        var status = a.Participation?.ToLower() == "present" ? "Có mặt" : "Vắng mặt";
                        var note = a.Note ?? "Không có ghi chú";
                        var homework = a.Homework ?? "Không có thông tin";
                        var focus = a.Focus ?? "Không rõ";

                        sb.AppendLine($"- {subject}: {status}");
                        sb.AppendLine($"  • Nhận xét: {note}");
                        sb.AppendLine($"  • Bài tập: {homework}");
                        sb.AppendLine($"  • Mức độ tập trung: {focus}");
                    }
                }
                else
                {
                    sb.AppendLine("- Không có tiết học nào hoặc chưa có dữ liệu điểm danh.");
                }

                // 📆 LỊCH HỌC SẮP TỚI
                sb.AppendLine("\n📆 Lịch học sắp tới:");
                foreach (var date in daysToCheck)
                {
                    var vnDay = TranslateDay(date.DayOfWeek);
                    var dateStr = date.ToString("dd/MM/yyyy");

                    var schedule = await _context.Courses
                        .Include(c => c.Teacher)
                        .ThenInclude(t => t.User)
                        .Where(c => c.ClassId == student.ClassId && c.DayOfWeek == vnDay)
                        .OrderBy(c => c.StartTime)
                        .ToListAsync();

                    if (schedule.Any())
                    {
                        sb.AppendLine($"- {vnDay} ({dateStr}):");
                        foreach (var c in schedule)
                        {
                            var subject = c.SubjectName;
                            var start = c.StartTime?.ToString("HH:mm") ?? "?";
                            var end = c.EndTime?.ToString("HH:mm") ?? "?";
                            var teacher = c.Teacher?.User?.FullName ?? "Không rõ";

                            sb.AppendLine($"  + {subject} ({start} - {end}) - GV: {teacher}");
                        }
                    }
                    else
                    {
                        sb.AppendLine($"- {vnDay} ({dateStr}): Không có tiết học nào.");
                    }
                }
            }

            sb.AppendLine("\n---\nHãy sử dụng thông tin trên để trả lời các câu hỏi từ phụ huynh bằng tiếng Việt, đúng ngữ cảnh, ngắn gọn và rõ ràng.");
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
