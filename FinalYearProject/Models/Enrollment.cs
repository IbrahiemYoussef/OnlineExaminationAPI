using System;
using System.Collections.Generic;

#nullable disable

namespace FinalYearProject.Models
{
    public partial class Enrollment
    {
        public int CourseId { get; set; }
        public int ApplicationUserId { get; set; }
        public string Grade { get; set; }
        public int TotalMarks { get; set; }

        public virtual ApplicationUser ApplicationUsers { get; set; }
        public virtual Course Courses { get; set; }
    }
}
