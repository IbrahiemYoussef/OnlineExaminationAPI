using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalYearProject.Models
{
    public class ScheduleWithCourse
    {
        //public int Id { get; set; }
        public int schedule_id { get; set; }
        public int course_id { get; set; }
        public virtual Course Course { get; set; }
        public virtual Schedule Schedule { get; set; }
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }
    }
}
