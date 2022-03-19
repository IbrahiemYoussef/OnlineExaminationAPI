using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalYearProject.Models.DTOs
{
    public class StudentCoursesDTO
    {
        public int CourseId { get; set; }  
        public string CourseName { get; set; } 
        public DateTime StartTime { get; set; }
        public int DurationInMinutes { get; set; }
        public bool isExaminated { get; set; }
    }
}
