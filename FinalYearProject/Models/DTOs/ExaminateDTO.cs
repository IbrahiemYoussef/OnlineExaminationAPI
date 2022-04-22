using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalYearProject.Models.DTOs
{
    public class ExaminateDTO
    {
        public string std_id { get; set; }
        public int course_id { get; set; }
        public int total_num_of_questions { get; set; }
        public List<AnswerDTO>? answers { get; set; }
    }
}
