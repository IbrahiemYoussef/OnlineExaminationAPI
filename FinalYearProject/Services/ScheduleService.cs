using FinalYearProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalYearProject.Services
{
    public class ScheduleService
    {
        private mydbcon _context;
        
        public ScheduleService(mydbcon context)
        {
            _context = context;
            
        }
        public dynamic CreateSchedule()
        {
            List <Course> mycourses = _context.Courses.Where(x => x.Is_open == true).ToList();
            // error check
            List<Course> badCourses = new List<Course>();
            foreach ( var course in mycourses)
            {
                var res = _context.ExamDetails.Where(x => x.Course_id == course.Id).FirstOrDefault();
                if (res==null)
                { 
                    badCourses.Add(course);
                }
            }
            if (badCourses != null)
            {
                return badCourses;
            }
            else
            {
                return mycourses;
            }
        }
    }
}
