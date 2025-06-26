namespace EduConnect.DTO
{
    public class AttendanceCreate
    {
        public string? AtID { get; set; }
        public string StudentId { get; set; }
        public string CourseId { get; set; }
        public string? Participation { get; set; }
        public string? Note { get; set; }
        public bool? Homework { get; set; }
        public bool? Focus { get; set; }
    }
}
