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

        public GlobalResponseDTO GetProfessorCourses(string professor_id)
        {
            IQueryable<ProfessorCoursesDTO> query;
            try
            {
                query =
                    from enroll in _context.EnrollementProfessors.Where(x => x.ApplicationUserId == professor_id)
                    join schedule_course in _context.ScheduleWithCourses
                        on enroll.CourseId equals schedule_course.course_id
                    join course in _context.Courses
                        on schedule_course.course_id equals course.Id
                    join examdetail in _context.ExamDetails
                        on course.Id equals examdetail.Course_id
                    select new ProfessorCoursesDTO
                    {
                        CourseId=course.Id,
                        CourseName=course.Name,
                        CourseCode=course.CourseCode,
                        CreditHrs=course.CreditHrs,
                        FLevel_Id=course.FLevel_Id,
                        IsConfigured = Convert.ToBoolean(examdetail.NumberOfQuestions) ? true : false
                    };

            }
            catch (Exception ex)
            {
                return new GlobalResponseDTO(false, ex.Message, null);
            }
            //full join enrollments with SCW-MN filter at last
            return new GlobalResponseDTO(true, "Fetched Professor table successfully", query);

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
                        courseId=course.Id,
                        courseName = course.Name,
                        startTime = schedule_course.StartTime,
                        durationInMinutes = examdetail.ExamDurationInMinutes,
                        isExaminated = enroll.isExaminated,
                        currentMarks=enroll.CurrentMarks,
                        totalMarks=enroll.TotalMarks

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
