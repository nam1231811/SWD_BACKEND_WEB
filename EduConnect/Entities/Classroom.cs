using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EduConnect.Entities;

public class Classroom
{
    [Key] public String ClassId { get; set; } = Guid.NewGuid().ToString();
    public string? ClassName { get; set; }
    public String? TeacherId { get; set; }

    public virtual Teacher? Teacher { get; set; }

    // Quan hệ 1 lớp có nhiều học sinh
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}



