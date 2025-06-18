using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EduConnect.Entities;

public partial class ChatBotLog
{
    [Key] public String ChatId { get; set; } = Guid.NewGuid().ToString();

    public String? MessageId { get; set; }

    public String? ParentId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Title { get; set; }

    public virtual Message? Message { get; set; }

    public virtual Parent? Parent { get; set; }
}
