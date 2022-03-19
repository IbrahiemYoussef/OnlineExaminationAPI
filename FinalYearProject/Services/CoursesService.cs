using AutoMapper;
using FinalYearProject.Models;
using FinalYearProject.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalYearProject.Services
{
    public class CoursesService
    {
        private mydbcon _context;
        private readonly IMapper _mapper;
        public CoursesService(mydbcon context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<CourseDTO> GetManagedCourses(string prof_id)
        {
            var res= _context.Courses.Where(x => x.ApplicationUserId == prof_id).ToList();
            return _mapper.Map<List<CourseDTO>>(res);

        }

        public GlobalResponseDTO GetStudentCourses(string std_id)
        {
            var std_courses = _context.Enrollments.Where(x => x.ApplicationUserId == std_id).ToList();
            List<ScheduleWithCourse> SchCou = new List<ScheduleWithCourse>();
            List<string> mycourses = new List<string>();
            //to check he examinated or not
            List<bool> statues = new List<bool>();
            List<Schedule> CoursesTime = new List<Schedule>();

            foreach (var sch in std_courses)
            {
                 var std_sch = _context.ScheduleWithCourse.Where(x => x.course_id == sch.CourseId).FirstOrDefault();
                if (sch.TotalMarks == null)
                    statues.Add(false);
                else
                    statues.Add(true);
                SchCou.Add(std_sch);
            }
            
            
            foreach (var examtime in SchCou)
            {
                var std_corse = _context.Courses.Where(x => x.Id == examtime.course_id).Select(x => new {x.Id, x.Name }).FirstOrDefault().ToString();
                mycourses.Add(std_corse);
                var coursedate = _context.Schedules.Where(x => x.Id == examtime.schedule_id).FirstOrDefault();
                CoursesTime.Add(coursedate);
            }
            var examdetail = CoursesTime.Select(x => new { x.StartTime, x.Duration }).ToList();

            //statues + mycourses + examdetail
            List<StudentCoursesDTO> result = new List<StudentCoursesDTO>();
            for (int i = 0; i < mycourses.Count; i++)
            {
                result.Add(new StudentCoursesDTO()
                {
                    CourseId=mycourses[i].Cou
                });
            }
            List<object> objectlist = mycourses.Cast<object>().Concat(examdetail).ToList();
            return new GlobalResponseDTO(true, "successed",
                        new
                        {
                            CoursesData=objectlist
                        });
            //to be contiunued...........
        }


    }
}
