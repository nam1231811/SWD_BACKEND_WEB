namespace EduConnect.DTO
{
    public class TermCreated
    {
        public string? TermID { get; set; }
        public string? Mode { get; set; }
        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public DateTime? CreatedAt { get; set; }

        public String? ReportId { get; set; }
    }
}
