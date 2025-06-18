using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace EduConnect.Entities;

public partial class User
{
    [Key] public String UserId { get; set; } = Guid.NewGuid().ToString();

    public string? Role { get; set; }

    public bool? IsActive { get; set; }

    public string? FullName { get; set; }

    public DateTime? CreateAt { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? PasswordHash { get; set; }

    public virtual ICollection<Parent> Parents { get; set; } = new List<Parent>();

    public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
}
