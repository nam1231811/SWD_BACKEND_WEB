namespace EduConnect.DTO
{
    public class CourseCreate
    {
        public string? ClassId { get; set; }
        public string? TeacherId { get; set; }
        public string? SemeId { get; set; }
        public TimeOnly? StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
        public string? DayOfWeek { get; set; }
        public string? Status { get; set; }
    }
}
