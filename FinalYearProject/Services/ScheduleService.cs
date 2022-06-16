using AutoMapper;
using FinalYearProject.Models;
using FinalYearProject.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalYearProject.Services
{
    public class ScheduleService
    {
        private mydbcon _context;
        private readonly IMapper _mapper;
        public ScheduleService(mydbcon context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        public GlobalResponseDTO CreateSchedule(string startdatee)
        {

            DateTime startdate = Convert.ToDateTime(startdatee);
            //foreach for bad courses
            if (DateTime.Now < startdate)
            {
                List<Faculty> Allfaculties = _context.Faculties.ToList();
                Dictionary<object, object> mydict = new Dictionary<object, object>();
                Dictionary<object, object> mydictt = new Dictionary<object, object>();
                foreach (var fac in Allfaculties)
                {
                    var checksch = _context.Schedules.Where(x => x.FacultyId == fac.Id).FirstOrDefault();
                    if (checksch != null)
                        return new GlobalResponseDTO(false, " The faculties already have thier schedule", null);
                    List<Course> mycourses = _context.Courses.Where(x => x.Is_open == true && x.Faculty_id == fac.Id).ToList();
                    // error check
                    List<Course> badCourses = new List<Course>();
                    List<Course> noquestionBankcour = new List<Course>();
                    foreach (var course in mycourses)
                    {
                        var res = _context.ExamDetails.Where(x => x.Course_id == course.Id).FirstOrDefault();

                        if (res.isQuestionBankConfigured == false)
                        {
                            noquestionBankcour.Add(course);
                        }
                        if (res == null)
                        {
                            badCourses.Add(course);
                        }
                    }
                    if (noquestionBankcour.Count != 0)
                    {
                        var list = _mapper.Map<List<CourseDTO>>(noquestionBankcour);
                        mydictt.Add(fac.Name, list);
                    }
                    if (badCourses.Count != 0)
                    {
                        var list = _mapper.Map<List<CourseDTO>>(badCourses);
                        mydict.Add(fac.Name, list);
                    }
                }
                if (mydictt.Values.Count != 0)
                {
                    return new GlobalResponseDTO(false, "Some courses doesn't have question banks in faculty of the following: ", mydictt);
                }
                if (mydict.Values.Count != 0)
                {
                    return new GlobalResponseDTO(false, "Some courses doesn't have examdetails in faculty of the following: ", mydict);
                }
                foreach (var fac in Allfaculties)
                {
                    List<Course> mycourses = _context.Courses.Where(x => x.Is_open == true && x.Faculty_id == fac.Id).ToList();
                    var firstExamDate = startdate.Date;
                    firstExamDate = firstExamDate.AddHours(9);
                    var _schedule = new Schedule()
                    {
                        Is_set = true,
                        FacultyId = fac.Id

                    };
                    _context.Schedules.Add(_schedule);
                    _context.SaveChanges();
                    var _SchdlCoursee = new ScheduleWithCourse()
                    {
                        schedule_id = _schedule.Id,
                        course_id = mycourses[0].Id,
                        StartTime = firstExamDate,
                        Duration = _context.ExamDetails.Where(x => x.Course_id == mycourses[0].Id).Select(x => x.ExamDurationInMinutes).FirstOrDefault()
                    };
                    _context.ScheduleWithCourses.Add(_SchdlCoursee);
                    var len = mycourses.Count();
                    for (int i = 1; i < len; i++)
                    {
                        if (firstExamDate.Hour >= 13)
                        {
                            firstExamDate = firstExamDate.AddHours(-4);
                        }
                        if (mycourses[i].FLevel_Id == mycourses[i - 1].FLevel_Id)
                        {
                            firstExamDate = firstExamDate.AddDays(2);
                        }
                        else
                        {
                            firstExamDate = firstExamDate.AddHours(4);
                        }

                        var _SchdlCourse = new ScheduleWithCourse()
                        {
                            schedule_id = _schedule.Id,
                            course_id = mycourses[i].Id,
                            StartTime = firstExamDate,
                            Duration = _context.ExamDetails.Where(x => x.Course_id == mycourses[i].Id).Select(x => x.ExamDurationInMinutes).FirstOrDefault()
                        };
                        _context.ScheduleWithCourses.Add(_SchdlCourse);
                    }
                    _context.SaveChanges();
                    //FLevels FirstLevel = _context.FLevels.Where(x => x.Level_name == "Level1").FirstOrDefault();
                    //FLevels ThirdLevel = _context.FLevels.Where(x => x.Level_name == "Level3").FirstOrDefault();
                    //FLevels SecLevel = _context.FLevels.Where(x => x.Level_name == "Level2").FirstOrDefault();
                    //FLevels FourthLevel = _context.FLevels.Where(x => x.Level_name == "Level4").FirstOrDefault();
                    //var coursesL1L3 = mycourses.Where(x => x.FLevel_Id == FirstLevel.Id || x.FLevel_Id == ThirdLevel.Id).ToList();
                    //var coursesL2L4 = mycourses.Where(x => x.FLevel_Id == SecLevel.Id || x.FLevel_Id == FourthLevel.Id).ToList();

                }
                return new GlobalResponseDTO(true, "created schedule successfully", null);
            }
            else
            {
                return new GlobalResponseDTO(false, "invalid date time", null);

            }
        }
        public GlobalResponseDTO GetSchedule()
        {
            List<Faculty> Allfaculties = _context.Faculties.ToList();
            Dictionary<object, object> mydict = new Dictionary<object, object>();
            List<object> listSchedule = new List<object>();
            foreach (var fac in Allfaculties)
            {
                var schedul = _context.Schedules.Where(x => x.FacultyId == fac.Id).FirstOrDefault();
                if (schedul == null)
                {
                    return new GlobalResponseDTO(false, "there is no schedule", null);
                }
                else
                {
                    ////listSchedule.Add(fac.Name);
                    //var facultyschedule = _context.ScheduleWithCourses.Where(x => x.schedule_id == schedul.Id).ToList();
                    ////listSchedule.Add(facultyschedule);
                    //mydict.Add(fac.Name, facultyschedule);
                    var query = from i in _context.Schedules.Where(x => x.FacultyId == fac.Id)
                                join schedule_course in _context.ScheduleWithCourses
                                on i.Id equals schedule_course.schedule_id
                                join Course in _context.Courses
                                on schedule_course.course_id equals Course.Id
                                select new
                                {
                                    course_id = schedule_course.course_id,
                                    schedule_id = schedule_course.schedule_id,
                                    StartTime = schedule_course.StartTime,
                                    Duration = schedule_course.Duration,
                                    CourseName = Course.Name,
                                    EndTime = schedule_course.StartTime.AddMinutes(schedule_course.Duration),
                                    CourseCode = Course.CourseCode

                                };
                    mydict.Add(fac.Name, query);
                }
            }
            return new GlobalResponseDTO(true, "fetched all schedules successfully", mydict);
        }
        public GlobalResponseDTO deletSchedule()
        {
            var sch = _context.Schedules.ToList();
            if (sch == null)
                return new GlobalResponseDTO(false, "Schedules are empty", null);
            _context.Schedules.RemoveRange(sch);
            var schwithcour = _context.ScheduleWithCourses.ToList();
            _context.ScheduleWithCourses.RemoveRange(schwithcour);
            _context.SaveChanges();
            return new GlobalResponseDTO(true, "successfuly deleted schedules", null);
        }
    }
}
