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
        private ExamsService _examService;
        private mydbcon _context;
        private readonly IMapper _mapper;
        public CoursesService(mydbcon context, IMapper mapper, ExamsService examService)
        {
            _context = context;
            _mapper = mapper;
            _examService = examService;
        }

        public GlobalResponseDTO GetAllCourses()
        {
            List<Course> courses = _context.Courses.ToList<Course>();
            return new GlobalResponseDTO(true, "Fetched all courses in DB", courses);
        }
        public GlobalResponseDTO GetProfessorCourses(string prof_id)
        {
            return null;
            //under maintenance
            //var res= _context.Courses.Where(x => x.ApplicationUserId == prof_id).ToList();
            //List<ProfessorCoursesDTO> ProfCourses = new List<ProfessorCoursesDTO>();
            //foreach ( var i in res )
            //{
            //  var checkit=  _examService.GetExamdetailsByCourseId(i.Id);
            //    if (checkit == null)
            //    {
            //        ProfCourses.Add(new ProfessorCoursesDTO()
            //        {
            //            Id=i.Id,
            //            Name=i.Name,
            //            CreditHrs=i.CreditHrs,
            //            FLevel_Id=i.FLevel_Id,
            //            IsConfigured=false

            //        });
            //    }
            //    else
            //    {
            //        ProfCourses.Add(new ProfessorCoursesDTO()
            //        {
            //            Id = i.Id,
            //            Name = i.Name,
            //            CreditHrs = i.CreditHrs,
            //            FLevel_Id = i.FLevel_Id,
            //            IsConfigured = true
            //        });
            //    }
            //}
            //return new GlobalResponseDTO(true, "succeeded", ProfCourses);
            //return _mapper.Map<List<CourseDTO>>(res);

        }

        public GlobalResponseDTO GetStudentCourses(string student_id)
        {
            IQueryable<StudentScheduleDTO> query;
            try
            {
                query = 
                    from enroll in _context.Enrollments.Where(x => x.ApplicationUserId == student_id)
                    join schedule_course in _context.ScheduleWithCourses
                        on enroll.CourseId equals schedule_course.course_id
                    join course in _context.Courses
                        on schedule_course.course_id equals course.Id
                    join examdetail in _context.ExamDetails
                        on course.Id equals examdetail.Course_id
                    select new StudentScheduleDTO
                    {
                        CourseName = course.Name,
                        StartTime = schedule_course.StartTime,
                        DurationInMins = examdetail.ExamDurationInMinutes,
                        IsExaminated = enroll.isExaminated
                    };

            }
            catch(Exception ex)
            {
                return new GlobalResponseDTO(false, ex.Message, null);
            }
            //full join enrollments with SCW-MN filter at last
            return new GlobalResponseDTO (true,"Fetched student table successfully", query);
        }


    }
}
