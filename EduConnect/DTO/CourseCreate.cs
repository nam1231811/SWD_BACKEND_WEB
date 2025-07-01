namespace EduConnect.DTO
{
    public class CourseCreate
    {
        public string? ClassId { get; set; }
        public string? TeacherId { get; set; }
        public string? SemeId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? DayOfWeek { get; set; }
        public string? Status { get; set; }
    }
}
