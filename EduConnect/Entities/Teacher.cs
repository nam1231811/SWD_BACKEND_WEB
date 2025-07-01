using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EduConnect.Entities;

public partial class Teacher
{
    [Key] public String TeacherId { get; set; }
    public String? UserId { get; set; }
    public String? SubjectId { get; set; }
    public string? Status { get; set; }

    public string? FcmToken { get; set; }
    public string? Platform { get; set; }

    public virtual Classroom? Classroom { get; set; }
    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
    public virtual Subject? Subject { get; set; }
    public virtual User? User { get; set; }
}



