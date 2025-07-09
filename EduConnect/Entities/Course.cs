using System.ComponentModel.DataAnnotations;
using EduConnect.Entities;

public partial class Course
{
    [Key] public string CourseId { get; set; } = Guid.NewGuid().ToString();

    public string? ClassId { get; set; }
    public string? TeacherId { get; set; }
    public string? SemeId { get; set; }

    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string? DayOfWeek { get; set; }
    public string? Status { get; set; }
    public string? SubjectName { get; set; }

    public virtual Classroom? Class { get; set; }
    public virtual Teacher? Teacher { get; set; }
    public virtual Semester? Semester { get; set; }
}
