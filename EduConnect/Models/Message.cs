namespace EduConnect.Models
{
    public class Message
    {
        public int MessageID { get; set; }
        public string ResponseText { get; set; }
        public DateTime CreatedAt { get; set; }
        public string MessageText { get; set; }

        public ICollection<ChatBotLog> ChatBotLogs { get; set; }
    }
}
