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
        
        public ScheduleService(mydbcon context)
        {
            _context = context;
            
        }
        public GlobalResponseDTO CreateSchedule(DateTime startdate)
        {
            if ( DateTime.Now < startdate)
            { 
            List<Faculty> Allfaculties = _context.Faculties.ToList();
            foreach (var fac in Allfaculties)
            {
                List<Course> mycourses = _context.Courses.Where(x => x.Is_open == true && x.Faculty_id == fac.Id).ToList();
                // error check
                List<Course> badCourses = new List<Course>();
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
                    return new GlobalResponseDTO(false, "Some courses doesn't have examdetails", badCourses);
                }
                else
                {
                    var firstExamDate = startdate.AddHours(9);
                    var _schedule = new Schedule()
                    {
                        Is_set = true,
                        FacultyId = fac.Id

                    };
                    _context.Schedules.Add(_schedule);
                        var _SchdlCoursee = new ScheduleWithCourse()
                        {
                            schedule_id = _schedule.Id,
                            course_id = mycourses[0].Id,
                            StartTime = firstExamDate,
                            Duration = Convert.ToInt32(_context.ExamDetails.Where(x => x.Course_id == mycourses[0].Id).Select(x => x.ExamDurationInMinutes))
                        };
                        _context.ScheduleWithCourses.Add(_SchdlCoursee);
                        var len = mycourses.Count();
                    for (int i =1; i < len; i++)
                    {
                            if (firstExamDate.Hour >= 13)
                            {
                                firstExamDate = firstExamDate.AddHours(-4);
                            }
                            if( mycourses[i].FLevel_Id == mycourses[i-1].FLevel_Id)
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
                                Duration =Convert.ToInt32(_context.ExamDetails.Where(x => x.Course_id == mycourses[i].Id).Select(x => x.ExamDurationInMinutes))
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
                return new GlobalResponseDTO(true, "created schedule successfully", null);
            }
            else
            {
                return new GlobalResponseDTO(false, "invailed date time", null);
            }
            
        }
    }
}
