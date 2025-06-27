namespace EduConnect.DTO
{
    public class UpdateSemester
    {
        public string SemesterName { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string SchoolYearID { get; set; }
        public string Status { get; set; }
    }
}
