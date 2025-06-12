namespace EduConnect.Models
{
    public class Attendance
    {
        public int StudentID { get; set; }
        public int CourseID { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }

        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
