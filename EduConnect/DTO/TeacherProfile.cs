namespace EduConnect.DTO.Teacher;

public class TeacherProfile
{
    public string TeacherId { get; set; } = null!;
    public string? UserId { get; set; }
    public string? SubjectId { get; set; }
    public TimeOnly? StartTime { get; set; }
    public TimeOnly? EndTime { get; set; }
    public string? Status { get; set; }
}
