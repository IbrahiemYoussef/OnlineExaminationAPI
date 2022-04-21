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
        public DebugController(mydbcon context)
        {
            _context = context;
        }

        [HttpPost("EnrollStudent")]
        public IActionResult EnrollStudentByID(string id)
        {
            //it will give cycle error but all works -- search google
            IQueryable<int> courses_ids = _context.Courses.Select(c => c.Id);

            ApplicationUser user = _context.ApplicationUsers.Where(a => a.Id == id).FirstOrDefault();
            
            if (user == null)
                return  BadRequest(new GlobalResponseDTO(false,"Invalid ID",null));

            List<Enrollment> enrollments = new List<Enrollment>();
            foreach (int cid in courses_ids)
            {
                enrollments.Add(new Enrollment()
                {
                    ApplicationUserId = user.Id,
                    CourseId = cid
                });   
            }
              _context.Enrollments.AddRange(enrollments);
              _context.SaveChanges();

            return Ok(new GlobalResponseDTO(true, "Enrolled to all courses successfully", enrollments));
        }

    }
}
