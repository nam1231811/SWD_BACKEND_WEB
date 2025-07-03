namespace EduConnect.DTO.Teacher;

public class TeacherProfileDto
{
    public string TeacherId { get; set; }
    public string UserId { get; set; }

    public string? SubjectId { get; set; }
    public string? Status { get; set; }

    public string? FullName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime? CreateAt { get; set; }

    public string? UserImage { get; set; }


}
