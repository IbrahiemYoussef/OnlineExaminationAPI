using System;
using System.Collections.Generic;

#nullable disable

namespace FinalYearProject.Models
{
    public partial class StudentAnswer
    {
        public int? ApplicationUserId { get; set; }
        public int? ExamQuestionsId { get; set; }
        public string Answer { get; set; }
        public virtual ApplicationUser ApplicationUsers { get; set; }
        public virtual ExamQuestion ExamQuestions { get; set; }
    }
}
