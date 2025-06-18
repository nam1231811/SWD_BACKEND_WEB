using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EduConnect.Entities;

public partial class Parent
{
    [Key] public String ParentId { get; set; } = Guid.NewGuid().ToString();

    public String? UserId { get; set; }

    public virtual ICollection<ChatBotLog> ChatBotLogs { get; set; } = new List<ChatBotLog>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual User? User { get; set; }
}
