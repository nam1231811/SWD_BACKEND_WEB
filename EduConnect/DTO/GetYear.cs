namespace EduConnect.DTO
{
    public class GetYear
    {
        public string SchoolYearID { get; set; }
        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public string? Status { get; set; }
    }
}
