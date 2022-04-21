using System;
using System.Collections.Generic;

#nullable disable

namespace FinalYearProject.Models
{
    public partial class Enrollment
    {
        public int CourseId { get; set; }
        public string Grade { get; set; } 
        public int? CurrentMarks { get; set; }
        public int? TotalMarks { get; set; }
        public bool isExaminated { get; set; } = false;
        public string ApplicationUserId {get; set;}
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Course Course { get; set; }
    }
}
