namespace EduConnect.Models
{
    public class Score
    {
        public int ScoreID { get; set; }
        public int SubjectID { get; set; }
        public decimal ScoreValue { get; set; }

        public Subject Subject { get; set; }
    }
}
