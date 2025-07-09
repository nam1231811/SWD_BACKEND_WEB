using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EduConnect.Entities
{
    public partial class SchoolYear
    {
        [Key] public string SchoolYearId { get; set; } = Guid.NewGuid().ToString();

        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public string? Status { get; set; }

        public virtual ICollection<Semester> Semesters { get; set; } = new List<Semester>();
        public virtual ICollection<Classroom> Classrooms { get; set; } = new List<Classroom>();

    }
}
