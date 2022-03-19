using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalYearProject.Models.DTOs
{
    public class StudentCoursesDTO
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }
        public bool Statues { get; set; }
    }
}
