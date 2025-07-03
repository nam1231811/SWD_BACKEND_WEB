namespace EduConnect.DTO
{
    public class AttendanceNotification
    {
        public string UserId { get; set; }
        public string? FcmToken { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

    }
}
