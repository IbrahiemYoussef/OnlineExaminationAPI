using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalYearProject.Models.DTOs
{
    public class AnswerDTO
    {
        public int Id { get; set; }
        public string Answer { get; set; }
        public int TotalNumberOfQuestions { get; set; }
        public char Qtype { get; set; }
    }
}
