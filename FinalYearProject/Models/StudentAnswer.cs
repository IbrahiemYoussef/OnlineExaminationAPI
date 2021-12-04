using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace FinalYearProject.Models
{
    public partial class StudentAnswer
    {
        public int? ExamQuestionsId { get; set; }
        public string Answer { get; set; }
        public string? ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ExamQuestion ExamQuestion { get; set; }
    }
}
