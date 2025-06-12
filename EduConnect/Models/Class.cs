namespace EduConnect.Models
{
    public class Class
    {
        public int ClassID { get; set; }
        public string ClassName { get; set; }
        public int HomeRoomTeacherID { get; set; }
        public int StudentID { get; set; }

        public HomeRoomTeacher HomeRoomTeacher { get; set; }
        public Student Student { get; set; }
        public ICollection<Course> Courses { get; set; }
        public ICollection<Notification> Notifications { get; set; }
    }
}
