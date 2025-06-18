using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EduConnect.Entities;

public partial class Message
{
    [Key] public String MessageId { get; set; } = Guid.NewGuid().ToString();

    public string? ResponseText { get; set; }

    public string? MessageText { get; set; }

    public DateOnly? CreatedAt { get; set; }

    public virtual ICollection<ChatBotLog> ChatBotLogs { get; set; } = new List<ChatBotLog>();
}
