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

        //put course code ya ibrahiem
        public int CreditHrs { get; set; }
        public bool Is_open { get; set; }
        public int FLevel_Id { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public int Faculty_Id { get; set; }
        public virtual Faculty Faculty { get; set; }
        public virtual FLevels FLevels { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ExamDetails ExamDetails { get; set; }
        public virtual ScheduleWithCourse ScheduleWithCourse { get; set; }
    }
}
