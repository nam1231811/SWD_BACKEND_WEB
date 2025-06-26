using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EduConnect.Entities;

public partial class Teacher
{
    [Key] public String TeacherId { get; set; }
    public String? UserId { get; set; }

    public String? SubjectId { get; set; }

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    public string? Status { get; set; }

    public virtual Classroom? Classroom { get; set; } // chỉ chủ nhiệm 1 lớp

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual Subject? Subject { get; set; }

    public virtual User? User { get; set; }
}

