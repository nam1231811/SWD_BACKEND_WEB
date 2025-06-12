namespace EduConnect.Models
{
    public class SubjectTeacher
    {
        public int TeacherID { get; set; }
        public int? UserID { get; set; }
        public int SubjectID { get; set; }
        public int TermID { get; set; }

        public User User { get; set; }
        public Subject Subject { get; set; }
        public Term Term { get; set; }
        public ICollection<Course> Courses { get; set; }
        public ICollection<Notification> Notifications { get; set; }
    }
}
