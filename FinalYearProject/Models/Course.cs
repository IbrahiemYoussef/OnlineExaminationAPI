using System;
using System.Collections.Generic;

#nullable disable

namespace FinalYearProject.Models
{
    public partial class Course
    {
        public Course()
        {
            Enrollments = new HashSet<Enrollment>();
            
            Questions = new HashSet<Question>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CreditHrs { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public ExamDetails ExamDetails { get; set; }
    }
}
