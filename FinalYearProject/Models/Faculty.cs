using System;
using System.Collections.Generic;

#nullable disable

namespace FinalYearProject.Models
{
    public partial class Faculty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Schedule> Schedule { get; set; }
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}
