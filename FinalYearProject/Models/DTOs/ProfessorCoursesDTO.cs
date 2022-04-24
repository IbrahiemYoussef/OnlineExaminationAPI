using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalYearProject.Models.DTOs
{
    public class ProfessorCoursesDTO
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public int CreditHrs { get; set; }
        public int FLevel_Id { get; set; }
        public bool IsConfigured { get; set; }
    }
}
