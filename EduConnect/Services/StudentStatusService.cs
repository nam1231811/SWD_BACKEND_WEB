using System.Text;
using EduConnect.Data;
using EduConnect.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Services
{
    public class StudentStatusService
    {
        private readonly AppDbContext _context;

        public StudentStatusService(AppDbContext context)
        {
            _context = context;
        }

        // ✅ 1. Thông tin phụ huynh
        public async Task<string> GetParentInfoAsync(string parentId)
        {
            var sb = new StringBuilder();
            var parent = await _context.Parents.Include(p => p.User).FirstOrDefaultAsync(p => p.ParentId == parentId);

            if (parent != null)
            {
                sb.AppendLine("👨‍👩‍👧‍👦 Thông tin phụ huynh:");
                sb.AppendLine($"- Họ tên: {parent.User?.FullName}");
                sb.AppendLine($"- Email: {parent.User?.Email}");
                sb.AppendLine($"- Số điện thoại: {parent.User?.PhoneNumber}\n");
            }
            else
            {
                sb.AppendLine("⚠️ Không tìm thấy thông tin phụ huynh.\n");
            }

            return sb.ToString();
        }

        // ✅ 2. Thông tin cơ bản học sinh
        public async Task<string> GetBasicStudentInfoAsync(string parentId)
        {
            var sb = new StringBuilder();

            var students = await _context.Students
                .Include(s => s.Class)
                .ThenInclude(c => c.Teacher)
                .ThenInclude(t => t.User)
                .Where(s => s.ParentId == parentId)
                .ToListAsync();

            if (!students.Any())
                return "❌ Không tìm thấy học sinh nào thuộc phụ huynh này.";

            foreach (var student in students)
            {
                sb.AppendLine("🧒 Thông tin học sinh:");
                sb.AppendLine($"- Họ tên: {student.FullName}");
                sb.AppendLine($"- Mã số: {student.StudentId}");
                sb.AppendLine($"- Ngày sinh: {student.DateOfBirth?.ToString("dd/MM/yyyy") ?? "Không rõ"}");
                sb.AppendLine($"- Lớp: {student.Class?.ClassName ?? "Không rõ"}");
                sb.AppendLine($"- GVCN: {student.Class?.Teacher?.User?.FullName ?? "Không rõ"}\n");
            }

            return sb.ToString();
        }

        // ✅ 3. Lịch học theo ngày trong tuần
        public async Task<string> GetWeeklyScheduleAsync(string classId)
        {
            var sb = new StringBuilder();
            sb.AppendLine("📚 Thời khóa biểu trong tuần:");

            foreach (var day in Enum.GetValues<DayOfWeek>())
            {
                var vnDay = TranslateDay(day);

                var courses = await _context.Courses
                    .Include(c => c.Teacher)
                    .ThenInclude(t => t.User)
                    .Where(c => c.ClassId == classId && c.DayOfWeek == vnDay)
                    .OrderBy(c => c.StartTime)
                    .ToListAsync();

                if (courses.Any())
                {
                    sb.AppendLine($"- {vnDay}:");
                    foreach (var c in courses)
                    {
                        var subject = c.SubjectName;
                        var start = FormatVietnamTime(c.StartTime);
                        var end = FormatVietnamTime(c.EndTime);
                        var teacher = c.Teacher?.User?.FullName ?? "Không rõ";

                        sb.AppendLine($"  + {subject} ({start} - {end}) - GV: {teacher}");
                    }
                }
            }

            return sb.ToString();
        }

        // ✅ 4. Điểm số học sinh
        public async Task<string> GetStudentScoresAsync(string studentId)
        {
            var sb = new StringBuilder();

            var scores = await _context.Scores
                .Include(s => s.Subject)
                .Include(s => s.Semester)
                .Where(s => s.StudentId == studentId)
                .ToListAsync();

            if (!scores.Any())
                return string.Empty;

            sb.AppendLine("📊 Điểm số theo từng học kỳ và loại điểm:");

            var scoresBySemester = scores.GroupBy(s => s.Semester?.SemesterName ?? "Không rõ học kỳ");
            foreach (var semGroup in scoresBySemester)
            {
                sb.AppendLine($"\n📘 Học kỳ: {semGroup.Key}");

                var subjectGroups = semGroup.GroupBy(s => s.Subject?.SubjectName ?? "Không rõ môn");
                foreach (var subjGroup in subjectGroups)
                {
                    sb.AppendLine($"- Môn: {subjGroup.Key}");

                    var typeGroups = subjGroup.GroupBy(s => s.Type ?? "Không rõ loại điểm");
                    foreach (var typeGroup in typeGroups)
                    {
                        var type = typeGroup.Key;
                        var scoresText = string.Join(", ", typeGroup.Select(s => s.Score1?.ToString("0.##") ?? "?"));
                        sb.AppendLine($"  + {type}: {scoresText}");
                    }
                }
            }

            return sb.ToString();
        }

        // ✅ 5. Lịch học hôm nay
        public async Task<string> GetTodayLearningStatusAsync(Student student)
        {
            var sb = new StringBuilder();
            sb.AppendLine("📅 Tình hình học tập hôm nay:");

            var today = ConvertToVietnamTime(DateTime.UtcNow).Date;

            var allCourses = await _context.Courses
                .Include(c => c.Teacher).ThenInclude(t => t.User)
                .Where(c => c.ClassId == student.ClassId && c.StartTime.HasValue)
                .ToListAsync();

            // ✅ Chuyển startTime về giờ VN rồi mới so sánh ngày
            var courses = allCourses
                .Where(c => ConvertToVietnamTime(c.StartTime.Value).Date == today)
                .OrderBy(c => c.StartTime)
                .ToList();

            var attendances = await _context.Attendances
                .Include(a => a.Course)
                .Where(a => a.StudentId == student.StudentId && courses.Select(c => c.CourseId).Contains(a.CourseId))
                .ToListAsync();

            if (!courses.Any())
            {
                sb.AppendLine("- Không có tiết học nào.");
                return sb.ToString();
            }

            foreach (var course in courses)
            {
                var subject = course.SubjectName;
                var start = FormatVietnamTime(course.StartTime);
                var end = FormatVietnamTime(course.EndTime);
                var teacher = course.Teacher?.User?.FullName ?? "Không rõ";

                var attendance = attendances.FirstOrDefault(a => a.CourseId == course.CourseId);
                sb.AppendLine($"- {subject} ({start} - {end}) - GV: {teacher}");

                if (attendance != null)
                {
                    var status = attendance.Participation?.ToLower() == "present" ? "Có mặt" : "Vắng mặt";
                    sb.AppendLine($"  • Trạng thái: {status}");
                    sb.AppendLine($"  • Nhận xét: {attendance.Note ?? "Không có ghi chú"}");
                    sb.AppendLine($"  • Bài tập: {attendance.Homework ?? "Không có thông tin"}");
                    sb.AppendLine($"  • Mức độ tập trung: {attendance.Focus ?? "Không rõ"}");
                }
                else
                {
                    sb.AppendLine("  • Chưa có dữ liệu điểm danh.");
                }
            }

            return sb.ToString();
        }

        // ✅ 6. Tình hình học hôm nay
        public async Task<string> GetTodayStatusSummaryAsync(Student student)
        {
            var sb = new StringBuilder();
            var today = ConvertToVietnamTime(DateTime.UtcNow).Date;

            // Lấy toàn bộ tiết học của hôm nay
            var courses = await _context.Courses
                .Include(c => c.Teacher).ThenInclude(t => t.User)
                .Where(c => c.ClassId == student.ClassId && c.StartTime.HasValue)
                .ToListAsync();

            var todayCourses = courses
                .Where(c => ConvertToVietnamTime(c.StartTime.Value).Date == today)
                .OrderBy(c => c.StartTime)
                .ToList();

            if (!todayCourses.Any())
                return $"📅 Hôm nay ({today:dd/MM/yyyy}) học sinh **{student.FullName}** không có tiết học nào.";

            // Lấy điểm danh tương ứng
            var attendances = await _context.Attendances
                .Where(a => a.StudentId == student.StudentId && todayCourses.Select(c => c.CourseId).Contains(a.CourseId))
                .ToListAsync();

            // Kiểm tra xem có dữ liệu điểm danh nào không
            var attendedCourses = todayCourses
                .Where(c => attendances.Any(a => a.CourseId == c.CourseId))
                .ToList();

            if (!attendedCourses.Any())
            {
                return $"📅 Hôm nay ({today:dd/MM/yyyy}), học sinh **{student.FullName}** có {todayCourses.Count} tiết học nhưng hiện **chưa có dữ liệu điểm danh** nào được ghi nhận.";
            }

            sb.AppendLine($"📅 **Tình hình học tập hôm nay ({today:dd/MM/yyyy}) của {student.FullName}:**\n");

            int index = 1;
            foreach (var course in attendedCourses)
            {
                var start = ConvertToVietnamTime(course.StartTime.Value).ToString("HH:mm");
                var end = ConvertToVietnamTime(course.EndTime.Value).ToString("HH:mm");
                var teacherName = course.Teacher?.User?.FullName ?? "Không rõ";
                var subject = course.SubjectName ?? "Không rõ";

                var attendance = attendances.FirstOrDefault(a => a.CourseId == course.CourseId);

                sb.AppendLine($"🔹 Tiết {index++}: {subject} ({start} - {end})");
                sb.AppendLine($"  • Giáo viên: {teacherName}");

                if (attendance != null)
                {
                    var status = attendance.Participation?.ToLower() == "present" ? "Có mặt" : "Vắng mặt";
                    sb.AppendLine($"  • Trạng thái: {status}");
                    sb.AppendLine($"  • Nhận xét: {attendance.Note ?? "Không có ghi chú"}");
                    sb.AppendLine($"  • Bài tập: {attendance.Homework ?? "Không có thông tin"}");
                    sb.AppendLine($"  • Mức độ tập trung: {attendance.Focus ?? "Không rõ"}");
                }

                sb.AppendLine(); // dòng trống giữa các tiết
            }

            return sb.ToString();
        }

        // ✅ 7. Lịch học sắp tới
        public async Task<string> GetUpcomingScheduleAsync(Student student)
        {
            var sb = new StringBuilder();

            var today = ConvertToVietnamTime(DateTime.UtcNow).Date;
            var daysToCheck = new[] { today.AddDays(1), today.AddDays(7) };

            foreach (var date in daysToCheck)
            {
                var vnDay = TranslateDay(date.DayOfWeek);
                var dateStr = date.ToString("dd/MM/yyyy");

                var allCourses = await _context.Courses
                    .Include(c => c.Teacher).ThenInclude(t => t.User)
                    .Where(c => c.ClassId == student.ClassId && c.StartTime.HasValue)
                    .ToListAsync();

                var schedule = allCourses
                    .Where(c => ConvertToVietnamTime(c.StartTime.Value).Date == date.Date)
                    .OrderBy(c => c.StartTime)
                    .ToList();

                sb.AppendLine($"\n📆 LỊCH HỌC NGÀY {vnDay.ToUpper()} ({dateStr}):");
                sb.AppendLine($"🧒 Học sinh: {student.FullName} ({student.StudentId})");
                sb.AppendLine($"🏫 Lớp: {student.Class?.ClassName ?? "Không rõ"}");

                if (!schedule.Any())
                {
                    sb.AppendLine("❌ Không có tiết học nào.");
                    continue;
                }

                int count = 1;
                foreach (var c in schedule)
                {
                    var subject = c.SubjectName;
                    var start = FormatVietnamTime(c.StartTime);
                    var end = FormatVietnamTime(c.EndTime);
                    var teacher = c.Teacher?.User?.FullName ?? "Không rõ";

                    sb.AppendLine($"\n{count}️⃣ Môn: {subject}");
                    sb.AppendLine($"🕒 Thời gian: {start} - {end}");
                    sb.AppendLine($"👨‍🏫 Giáo viên: {teacher}");
                    count++;
                }
            }

            return sb.ToString();
        }

        // ✅ 8. Tổng hợp context đầy đủ (nếu thật sự cần)
        public async Task<string> GenerateStudentContextByTypeAsync(string parentId, string type)
        {
            var sb = new StringBuilder();

            // Lấy phụ huynh và học sinh
            var parentInfo = await GetParentInfoAsync(parentId);
            var students = await _context.Students.Include(s => s.Class).Where(s => s.ParentId == parentId).ToListAsync();

            if (!students.Any())
                return "❌ Không tìm thấy học sinh nào cho phụ huynh này.";

            sb.AppendLine(parentInfo);

            foreach (var student in students)
            {
                sb.AppendLine("=========================================");
                sb.AppendLine($"🧒 Học sinh: {student.FullName} ({student.StudentId})");

                switch (type)
                {
                    case "scores":
                        sb.AppendLine(await GetStudentScoresAsync(student.StudentId));
                        break;
                    case "schedule":
                        sb.AppendLine(await GetWeeklyScheduleAsync(student.ClassId));
                        break;
                    case "today":
                        sb.AppendLine(await GetTodayLearningStatusAsync(student));
                        break;
                    case "upcoming":
                        sb.AppendLine(await GetUpcomingScheduleAsync(student));
                        break;
                    case "info":
                        sb.AppendLine(await GetBasicStudentInfoAsync(parentId));
                        break;
                    default:
                        sb.AppendLine("⚠️ Không rõ loại yêu cầu.");
                        break;
                }
            }

            return sb.ToString();
        }

        // 🧩 Helper
        private string TranslateDay(DayOfWeek day) => day switch
        {
            DayOfWeek.Monday => "Thứ Hai",
            DayOfWeek.Tuesday => "Thứ Ba",
            DayOfWeek.Wednesday => "Thứ Tư",
            DayOfWeek.Thursday => "Thứ Năm",
            DayOfWeek.Friday => "Thứ Sáu",
            DayOfWeek.Saturday => "Thứ Bảy",
            DayOfWeek.Sunday => "Chủ Nhật",
            _ => day.ToString()
        };

        private static DateTime ConvertToVietnamTime(DateTime utcTime)
        {
            var vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(utcTime, vnTimeZone);
        }

        private static string FormatVietnamTime(DateTime? utcDateTime)
        {
            if (!utcDateTime.HasValue) return "Không rõ";
            var localTime = ConvertToVietnamTime(utcDateTime.Value);
            return localTime.ToString("HH:mm");
        }
    }
}
