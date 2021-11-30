using System;
using System.Collections.Generic;

#nullable disable

namespace FinalYearProject.Models
{
    public partial class StudentAnswer
    {
        public int? StudentId { get; set; }
        public int? ExamQuestionsId { get; set; }
        public string Answer { get; set; }

        public virtual ExamQuestion ExamQuestions { get; set; }
    }
}
