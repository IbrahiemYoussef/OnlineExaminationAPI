using System;
using System.Collections.Generic;

#nullable disable

namespace FinalYearProject.Models
{
    public partial class Course
    {  
        public int Id { get; set; }
        public string Name { get; set; }
        public string CourseCode { get; set; }
        public int CreditHrs { get; set; }
        public bool Is_open { get; set; }
        public int FLevel_Id { get; set; }
        public int Faculty_id { get; set; }
        public virtual Faculty Faculty { get; set; }
        public virtual FLevels FLevels { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<EnrollementProfessor> EnrolementProfessors { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ExamDetails ExamDetails { get; set; }
        public virtual ICollection<ScheduleWithCourse> ScheduleWithCourses { get; set; }
    }
}
