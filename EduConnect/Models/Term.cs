namespace EduConnect.Models
{
    public class Term
    {
        public int TermID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string SemesterName { get; set; }
        public string SchoolYear { get; set; }
        public string Status { get; set; }

        public ICollection<Subject> Subjects { get; set; }
        public ICollection<SubjectTeacher> SubjectTeachers { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
