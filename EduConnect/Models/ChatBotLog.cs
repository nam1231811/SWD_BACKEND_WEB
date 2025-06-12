namespace EduConnect.Models
{
    public class ChatBotLog
    {
        public int HistoryID { get; set; }
        public int ParentID { get; set; }
        public DateTime CreatedAt { get; set; }
        public int MessageID { get; set; }
        public string Title { get; set; }

        public Parent Parent { get; set; }
        public Message Message { get; set; }
    }
}
