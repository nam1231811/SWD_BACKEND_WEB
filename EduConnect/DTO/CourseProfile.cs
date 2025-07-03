namespace EduConnect.DTO
{
    public class CourseProfile
    {
        public string CourseId { get; set; } = default!;
        public string? ClassId { get; set; }
        public string? TeacherId { get; set; }
        public string? SemeId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? DayOfWeek { get; set; }
        public string? Status { get; set; }
        public string? SubjectName { get; set; }
    }
}
