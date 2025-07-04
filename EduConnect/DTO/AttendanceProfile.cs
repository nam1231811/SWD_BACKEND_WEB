namespace EduConnect.DTO
{
    public class AttendanceProfile
    {
        public string AtID { get; set; } = null!;
        public string StudentId { get; set; } = null!;
        public string CourseId { get; set; } = null!;
        public string? Participation { get; set; }
        public string? Note { get; set; }
        public string? Homework { get; set; }
        public string? Focus { get; set; }
    }
}
