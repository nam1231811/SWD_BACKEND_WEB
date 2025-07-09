namespace EduConnect.DTO
{
    public class ClassProfile
    {
        public string? ClassId { get; set; }
        public string? ClassName { get; set; }
        public string? TeacherId { get; set; }
        public string? SchoolYearId { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
    }

}
