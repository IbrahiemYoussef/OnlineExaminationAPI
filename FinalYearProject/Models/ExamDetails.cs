﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalYearProject.Models
{
    public class ExamDetails
    {
        public int Id { get; set; }
        public int NumberOfQuestions { get; set; }
        public int NumberOfSingleMCQ { get; set; }
        public int NumberOfMultipleMCQ { get; set; }
        public int NumberOfTrueFalse { get; set; }
        public int NumberOfWritten { get; set; }
        public int NumberOfEasyQuestions { get; set; }
        public int NumberOfModQuestions { get; set; }
        public int NumberOfHardQuestions { get; set; }
        public int Course_id { get; set; }
        public Course Course { get; set; }
    }
}
