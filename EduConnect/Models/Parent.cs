namespace EduConnect.Models
{
    public class Parent
    {
        public int ParentID { get; set; }
        public int? StudentID { get; set; }
        public int? UserID { get; set; }

        public Student Student { get; set; }
        public User User { get; set; }
        public ICollection<ChatBotLog> ChatBotLogs { get; set; }
    }
}
