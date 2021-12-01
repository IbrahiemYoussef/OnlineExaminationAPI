using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalYearProject.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Isdisabled { get; set; }
       
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<StudentAnswer> StudentAnswers { get; set; }
    }
}
