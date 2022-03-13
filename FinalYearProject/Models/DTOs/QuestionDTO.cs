using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalYearProject.Models.DTOs
{
    public class QuestionDTO
    {
        public int Id { get; set; }
        public string Questionx { get; set; }
        public Char Qtype { get; set; }
        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public string D { get; set; }
        public string? Hint { get; set; }
        public int CourseId { get; set; }

    }
}
