using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace EduConnect.Entities;

public partial class Course
{
    [Key] public String CourseId { get; set; } = Guid.NewGuid().ToString();

    public String? ClassId { get; set; }

    public String? TeacherId { get; set; }

    public String? SemeId { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string? DayOfWeek { get; set; }

    public string? Status { get; set; }

    public string? SubjectName { get; set; }

    public virtual Classroom? Class { get; set; }

    public virtual Teacher? Teacher { get; set; }

    public virtual Semester? Semester { get; set; }
}
