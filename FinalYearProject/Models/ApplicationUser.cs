using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalYearProject.Models
{
    public class ApplicationUser: IdentityUser
    {
        
        //public int ApplicationUserId { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        
        [ForeignKey("FacultyId")]
        public int FacultyId { get; set; }
        public virtual Faculty Faculties { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<EnrolementProfessor> EnrollmentProfessors { get; set; }
    }
}
