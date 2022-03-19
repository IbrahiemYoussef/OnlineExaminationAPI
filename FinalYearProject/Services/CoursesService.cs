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
            var student_enrollments = _context.Enrollments.Where(x => x.ApplicationUserId == std_id).ToList();
            List<ScheduleWithCourse> SchCou = new List<ScheduleWithCourse>();
            List<Course> mycourses = new List<Course>();
            //to check he examinated or not
            List<bool> isExaminated = new List<bool>();
            List<Schedule> CoursesTime = new List<Schedule>();

            foreach (var enroll in student_enrollments)
            {
                if (enroll.TotalMarks == null)
                {
                    isExaminated.Add(false);
                }
                else
                {
                    isExaminated.Add(true);
                }
                var swc = _context.ScheduleWithCourse.Where(x => x.course_id == enroll.CourseId).FirstOrDefault();
                if(swc!=null)
                    SchCou.Add(swc);
            }
            
            
            foreach (var examtime in SchCou)
            {
                var std_corse = _context.Courses.Where(x => x.Id == examtime.course_id).FirstOrDefault();
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
                    CourseId=mycourses[i].Id,
                    CourseName = mycourses[i].Name,
                    StartTime=examdetail[i].StartTime,
                    DurationInMinutes = examdetail[i].Duration,
                    isExaminated = isExaminated[i]
                });
            }
            // List<object> objectlist = mycourses.Cast<object>().Concat(examdetail).ToList();
            return new GlobalResponseDTO(true, "succeeded",result);
        }


    }
}
