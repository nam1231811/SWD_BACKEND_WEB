using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EduConnect.Entities;

public partial class Score
{
    [Key] public String ScoreId { get; set; } = Guid.NewGuid().ToString();

    public String? SubjectId { get; set; }

    public decimal? Score1 { get; set; }

    public virtual Subject? Subject { get; set; }
}
