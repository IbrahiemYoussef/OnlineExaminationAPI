using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalYearProject.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        
        [ForeignKey("FacultyId")]
        public int FacultyId { get; set; }
        public virtual Faculty Faculties { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<StudentAnswer> StudentAnswers { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
