using System;
using System.Collections.Generic;

#nullable disable

namespace FinalYearProject.Models
{
    public partial class Question
    {
        public Question()
        {
            ExamQuestions = new HashSet<ExamQuestion>();
        }

        public int Id { get; set; }
        public string Questionx { get; set; }
        public string Answer { get; set; }
        public string? Hint { get; set; }
        public string? Goal { get; set; }
        
        public int CourseId { get; set; }
        public string Difficulty { get; set; }

        public virtual Course Course { get; set; }
        public virtual ICollection<ExamQuestion> ExamQuestions { get; set; }
    }
}
