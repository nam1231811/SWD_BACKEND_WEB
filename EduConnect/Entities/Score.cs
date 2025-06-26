using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EduConnect.Entities;

public partial class Score
{
    [Key] public string ScoreId { get; set; } = Guid.NewGuid().ToString();

    public string? SubjectId { get; set; }

    public string? StudentId { get; set; }  

    public decimal? Score1 { get; set; }

    public virtual Subject? Subject { get; set; }

    public virtual Student? Student { get; set; }  
}
