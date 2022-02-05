using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalYearProject.Models.DTOs
{
    public class ExamDetailsDTO
    {
        public int NumberOfQuestions { get; set; }
        public int NumberOfEasyQuestions { get; set; }
        public int NumberOfModQuestions { get; set; }
        public int NumberOfHardQuestions { get; set; }
        public string TypeOfQuestions { get; set; }
        public int Course_id { get; set; }
    }
}
