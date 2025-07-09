using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EduConnect.Entities;

public class Classroom
{
    [Key] public String ClassId { get; set; } = Guid.NewGuid().ToString();
    public string? ClassName { get; set; }
    public String? TeacherId { get; set; }

    public string? SchoolYearId { get; set; }           
    public DateOnly? StartDate { get; set; }           
    public DateOnly? EndDate { get; set; }              

    public virtual Teacher? Teacher { get; set; }
    public virtual SchoolYear? SchoolYear { get; set; } 

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
}
