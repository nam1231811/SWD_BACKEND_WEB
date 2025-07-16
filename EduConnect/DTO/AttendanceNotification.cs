namespace EduConnect.DTO
{
    public class AttendanceNotification
    {
        public string? FcmToken { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string? Platform { get; set; }

    }
}
