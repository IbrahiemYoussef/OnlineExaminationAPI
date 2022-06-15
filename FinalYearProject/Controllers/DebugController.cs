using FinalYearProject.Models;
using FinalYearProject.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalYearProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DebugController : ControllerBase
    {
        private readonly mydbcon _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public DebugController(mydbcon context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private async Task<string> getUserRole(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var rolenames = await _userManager.GetRolesAsync(user);
            return rolenames[0];
        }

        [HttpPost("EnrollStudents")]
        public IActionResult EnrollStudents()
        {
            //it will give cycle error but all works -- search google
            List<int> courses_ids = _context.Courses.Select(c => c.Id).ToList();

            List<ApplicationUser> users = _context.ApplicationUsers.ToList();

            foreach (ApplicationUser user in users.ToList())
            {
                bool ok = getUserRole(user.UserName).Result == UserRoles.Student;
                if (!ok)
                    users.Remove(user);
            }

            if (users == null)
                return  BadRequest(new GlobalResponseDTO(false,"No students in the database",null));

            List<List<Enrollment>> all_enrollments = new List<List<Enrollment>>();
            foreach(ApplicationUser user in users)
            {
                List<Enrollment> enrollments = new List<Enrollment>();

                foreach (int cid in courses_ids)
                {
                    enrollments.Add(new Enrollment()
                    {
                        ApplicationUserId = user.Id,
                        CourseId = cid
                    });
                }
                all_enrollments.Add(enrollments);
                _context.Enrollments.AddRange(enrollments);
                _context.SaveChanges();
            }
              
            return Ok(new GlobalResponseDTO(true, "Enrolled students to all courses successfully", all_enrollments));
        }

        [HttpPost("EnrollProfessors")]
        public IActionResult EnrollProfessors()
        {
            //it will give cycle error but all works -- search google
            List<int> courses_ids = _context.Courses.Select(c => c.Id).ToList();
    
            List<ApplicationUser> users = _context.ApplicationUsers.ToList();

            foreach(ApplicationUser user in users.ToList())
            {
                bool ok = getUserRole(user.UserName).Result == UserRoles.Professor;
                if (!ok)
                    users.Remove(user);
            }

            if (users == null)
                return BadRequest(new GlobalResponseDTO(false, "No professors in the database", null));
            
            List<List<EnrollementProfessor>> all_enrollments = new List<List<EnrollementProfessor>>();
            for(int i=0;i<users.Count();i++)
            {
                List<EnrollementProfessor> enrollments = new List<EnrollementProfessor>();
                foreach (int cid in courses_ids)
                {
                    enrollments.Add(new EnrollementProfessor()
                    {
                        ApplicationUserId = users[i].Id,
                        CourseId = cid
                    });
                }
                all_enrollments.Add(enrollments);
                _context.EnrollementProfessors.AddRange(enrollments);
                _context.SaveChanges();
            }

            

            return Ok(new GlobalResponseDTO(true, "Enrolled professors to all courses successfully", all_enrollments));
        }


        [HttpPost("ClearStudentGrades")]
        public IActionResult ClearStudentGrades(string id)
        {
            IQueryable<Enrollment> enrollments =_context.Enrollments.Where(e => e.ApplicationUserId == id);
            foreach(Enrollment enrollment in enrollments)
            {
                enrollment.CurrentMarks = enrollment.TotalMarks = null; 
                enrollment.Grade = null;
                enrollment.isExaminated = false;
            }
            _context.SaveChanges();
            return Ok(new GlobalResponseDTO(true, "Reset successful for all of the student subjects", enrollments));
        }
        [HttpPost]
        [Route("PostFaculty")]
        public IActionResult PostFaculty (string name)
        {
            if (name == null)
                return Ok(new GlobalResponseDTO(false, "empty", null));
            var fac = new Faculty()
            {
                Name = name
            };
            _context.Faculties.Add(fac);
            _context.SaveChanges();
            return Ok(new GlobalResponseDTO(true, "created successfuly", null));
        }
        [HttpPut]
        [Route("updateDatetimeSchWithCour")]
        public IActionResult UpdatDate(int course_id,DateTime starttime)
        {
            var sche = _context.ScheduleWithCourses.Where(x => x.course_id == course_id);
            if (sche ==null)
                return Ok(new GlobalResponseDTO(false, "Wrong data", null));
            return Ok();
        }

    }
}
