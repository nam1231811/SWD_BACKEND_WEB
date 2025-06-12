namespace EduConnect.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int ParentID { get; set; }
        public string FullName { get; set; }

        public ICollection<Parent> Parents { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
        public ICollection<Class> Classes { get; set; }
    }
}
