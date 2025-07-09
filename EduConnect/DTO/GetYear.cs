namespace EduConnect.DTO
{
    public class GetYear
    {
        public string SchoolYearId { get; set; }
        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public string? Status { get; set; }
    }
}
