namespace EduConnect.DTO
{
    public class NotificationCreate
    {
        public string? TeacherId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ClassId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
