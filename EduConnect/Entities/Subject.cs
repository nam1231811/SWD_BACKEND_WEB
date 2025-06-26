using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EduConnect.Entities;

public partial class Subject
{
    [Key] public String SubjectId { get; set; } = Guid.NewGuid().ToString();

    public string? SubjectName { get; set; }

    public String? SemeId { get; set; }

    public virtual ICollection<Score> Scores { get; set; } = new List<Score>();

    public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();

    public virtual Semester? Semester { get; set; }
}
