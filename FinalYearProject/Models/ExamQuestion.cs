using System;
using System.Collections.Generic;

#nullable disable

namespace FinalYearProject.Models
{
    public partial class ExamQuestion
    {
        public ExamQuestion()
        {
            StudentAnswers = new HashSet<StudentAnswer>();
        }

        public int Id { get; set; }
        public int? ExamId { get; set; }
        public int? QuestionId { get; set; }

        public virtual Exam Exam { get; set; }
        public virtual Question Question { get; set; }
        public virtual ICollection<StudentAnswer> StudentAnswers { get; set; }
    }
}
