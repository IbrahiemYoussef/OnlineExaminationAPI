using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalYearProject.Models.DTOs
{
    public class StudentScheduleDTO
    {
        public string CourseName { get; set; }
        public DateTime StartTime { get; set; }
        public int DurationInMins { get; set; }
        public bool IsExaminated { get; set; }
    }
}
