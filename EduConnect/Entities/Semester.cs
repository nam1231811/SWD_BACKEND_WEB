﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EduConnect.Entities;

public partial class Semester
{
    [Key] public string SemeId { get; set; } = Guid.NewGuid().ToString();

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? SemesterName { get; set; }

    public string? SchoolYearID { get; set; }

    public string? Status { get; set; }

    public virtual SchoolYear? SchoolYear { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();

    public virtual ICollection<Score> Scores { get; set; } = new List<Score>();
}