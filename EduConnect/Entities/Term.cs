    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.Identity.Client;

    namespace EduConnect.Entities;

        public partial class Term
        {
            [Key] public string? TermID { get; set; } = Guid.NewGuid().ToString();

            public string? Mode { get; set; } // tao report theo thang, ki, nam

            public DateTime? StartTime { get; set; }

            public DateTime? EndTime { get; set; }

            public DateTime? CreatedAt { get; set; }

            public String? ReportId { get; set; }
        
            public virtual Report? Report { get; set; }

        }

