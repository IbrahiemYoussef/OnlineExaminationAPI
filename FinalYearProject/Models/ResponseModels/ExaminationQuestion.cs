using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalYearProject.Models.ResponseModels
{
    public class ExaminationQuestion
    {
        public int Id { get; set; }
        public string Questionx { get; set; }
        public string? Answer { get; set; }
        public string? Hint { get; set; }
        public int CourseId { get; set; }

    }
}
