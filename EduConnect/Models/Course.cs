using System.Security.Claims;

namespace EduConnect.Models
{
    public class Course
    {
        public int CourseID { get; set; }
        public int ClassID { get; set; }
        public int TeacherID { get; set; }
        public int TermID { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string DayOfWeek { get; set; }
        public string Status { get; set; }

        public Class Class { get; set; }
        public SubjectTeacher Teacher { get; set; }
        public Term Term { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
    }
}
