namespace EduConnect.Models
{
    public class Notification
    {
        public int NotiID { get; set; }
        public int TeacherID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ClassID { get; set; }

        public SubjectTeacher Teacher { get; set; }
        public Class Class { get; set; }
    }
}
