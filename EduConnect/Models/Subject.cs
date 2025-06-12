namespace EduConnect.Models
{
    public class Subject
    {
        public int SubjectID { get; set; }
        public string SubjectName { get; set; }
        public int TermID { get; set; }

        public Term Term { get; set; }
        public ICollection<SubjectTeacher> SubjectTeachers { get; set; }
        public ICollection<Score> Scores { get; set; }
    }
}
