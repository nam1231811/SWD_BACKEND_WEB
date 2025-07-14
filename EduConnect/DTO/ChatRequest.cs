namespace EduConnect.DTO
{
    public class ChatRequest
    {
        public string ParentId { get; set; }  // ID của phụ huynh
        public string MessageText { get; set; }  // tin nhắn gốc phụ huynh gửi
    }
}
