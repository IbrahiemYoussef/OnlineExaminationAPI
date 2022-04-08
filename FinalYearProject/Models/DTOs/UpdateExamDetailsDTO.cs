using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalYearProject.Models.DTOs
{
    public class UpdateExamDetailsDTO
    {
        public int NumberOfQuestions { get; set; }
        public int NumberOfSingleMCQ { get; set; }
        public int NumberOfMultipleMCQ { get; set; }
        public int NumberOfTrueFalse { get; set; }
        public int NumberOfWritten { get; set; }
        public int NumberOfEasyQuestions { get; set; }
        public int NumberOfModQuestions { get; set; }
        public int NumberOfHardQuestions { get; set; }
        public string TypeOfQuestions { get; set; }
    }
}
