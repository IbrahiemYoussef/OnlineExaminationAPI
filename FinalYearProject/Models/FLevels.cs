using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalYearProject.Models
{
    public class FLevels
    {
        public int Id { get; set; }
        public string Level_name  { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
