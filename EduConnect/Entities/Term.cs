using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EduConnect.Entities;

public partial class Term
{
    [Key] public string? TermID { get; set; } = Guid.NewGuid().ToString();

    public string? Mode { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public DateTime? CreatedAt { get; set; }

    // Một Term có thể có nhiều Reports
    public virtual ICollection<Report>? Reports { get; set; }
}
