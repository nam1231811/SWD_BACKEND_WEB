using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EduConnect.Entities;

public partial class Student
{
    [Key] public String StudentId { get; set; } 

    public String? ParentId { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? FullName { get; set; }

    public String? ClassId { get; set; }

    public virtual Classroom? Class { get; set; }

    public virtual Parent? Parent { get; set; }
}
