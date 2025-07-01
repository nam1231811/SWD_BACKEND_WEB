using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EduConnect.Entities;

public partial class Report
{
    [Key] public String ReportId { get; set; } = Guid.NewGuid().ToString();

    public String? TeacherId { get; set; }

    public String? TermId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public String? ClassId { get; set; }

    public string? TeacherName { get; set; }

    public string? ClassName { get; set; }

    public virtual Classroom? Class { get; set; }

    public virtual Teacher? Teacher { get; set; }

    public virtual Term? Term { get; set; }

}
