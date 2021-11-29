using System;
using System.Collections.Generic;

#nullable disable

namespace FinalYearProject.Models
{
    public partial class Exam
    {
        public Exam()
        {
            ExamQuestions = new HashSet<ExamQuestion>();
        }

        public int Id { get; set; }
        public int Duration { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int CourseId { get; set; }

        public virtual Course Course { get; set; }
        public virtual ICollection<ExamQuestion> ExamQuestions { get; set; }
    }
}
