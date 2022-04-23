using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalYearProject.Models.DTOs
{
    public class StudentScheduleDTO
    {
        public int courseId { get; set; }
        public string courseName { get; set; }
        public DateTime startTime { get; set; }
        public int durationInMinutes { get; set; }
        public bool isExaminated { get; set; }
        public int? currentMarks { get; set; }
        public int? totalMarks { get; set; }
    }
}
