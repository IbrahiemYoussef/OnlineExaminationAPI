using FinalYearProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalYearProject.Services
{
    public class CoursesService
    {
        private mydbcon _context;
        public CoursesService(mydbcon context)
        {
            _context = context;
        }

        public List<Course> GetManagedCourses(int prof_id)
        {
            return _context.Courses.Where(x => x.ProfessorId == prof_id).ToList();
        }
    }
}
