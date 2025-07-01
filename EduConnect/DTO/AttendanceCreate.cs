namespace EduConnect.DTO
{
    public class AttendanceCreate
    {
        public string? AtID { get; set; }
        public string StudentId { get; set; }
        public string CourseId { get; set; }
        public string? Participation { get; set; }
        public string? Note { get; set; }
        public string? Homework { get; set; }
        public string? Focus { get; set; }
    }
}
