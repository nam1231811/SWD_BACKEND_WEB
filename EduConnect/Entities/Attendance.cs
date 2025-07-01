using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EduConnect.Entities;

public partial class Attendance
{
    public String StudentId { get; set; }

    public String CourseId { get; set; }

    public string? Participation { get; set; }

    public string? Note { get; set; }

    [Key] public String AtID { get; set; } = Guid.NewGuid().ToString();

    public string? Homework { get; set; }

    public string? Focus { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
