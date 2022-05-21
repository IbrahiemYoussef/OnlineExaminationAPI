using FinalYearProject.Models;
using FinalYearProject.Models.DTOs;
using FinalYearProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalYearProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        //private ScheduleService _ScheduleService;
        //public ScheduleController(ScheduleService scheduleservice)
        //{
        //    _ScheduleService = scheduleservice;
        //}
        //[Authorize(Roles = UserRoles.Admin)]
        private mydbcon _context;

        public ScheduleController(mydbcon context)
        {
            _context = context;

        }
        [HttpPost]
        [Route("CreateSchedule")]
        public GlobalResponseDTO CreateSchedule(DateTime startdate)
        {
            //foreach for bad courses
            if (DateTime.Now < startdate)
            {
                if (DateTime.Now < startdate)
                {
                    List<Faculty> Allfaculties = _context.Faculties.ToList();
                    foreach (var fac in Allfaculties)
                    {
                        List<Course> mycourses = _context.Courses.Where(x => x.Is_open == true && x.Faculty_id == fac.Id).ToList();
                        // error check
                        List<Course> badCourses = new List<Course>();
                        badCourses = null;
                        foreach (var course in mycourses)
                        {
                            var res = _context.ExamDetails.Where(x => x.Course_id == course.Id).FirstOrDefault();
                            if (res == null)
                            {
                                badCourses.Add(course);
                            }
                        }
                        if (badCourses != null)
                        {
                            return new GlobalResponseDTO(false, "Some courses doesn't have examdetails in faculty of " + fac.Name, badCourses);
                        }

                        {
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
                    }

                    
                }
                return new GlobalResponseDTO(true, "created schedule successfully", null);
            }
            else
            {
                return new GlobalResponseDTO(false, "invailed date time", null);

            }
        }
        [HttpGet]
        [Route("GetFacultySchedule")]
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
                return new GlobalResponseDTO(false, "there is a  faculty doesn't have schedule", null);
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
                                select new ScheduleWithCourse
                                {
                                    course_id = schedule_course.course_id,
                                    schedule_id = schedule_course.schedule_id,
                                    StartTime = schedule_course.StartTime,
                                    Duration = schedule_course.Duration
                                };
                    mydict.Add(fac.Name, query);
            }
            }
            return new GlobalResponseDTO(true, "fetched all schedules successfully", mydict);
        }
    }
}
