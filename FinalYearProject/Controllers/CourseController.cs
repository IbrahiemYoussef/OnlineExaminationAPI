using AutoMapper;
using FinalYearProject.Models.DTOs;
using FinalYearProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;

namespace FinalYearProject.Controllers
{
    [EnableCors(origins:"*",headers:"*",methods:"*")]
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private CoursesService _courseService;
        public CourseController(CoursesService courseService)
        {
            _courseService = courseService;
            
        }

        [HttpGet("GetAllCourses")]
        public IActionResult GetAllCourses()
        {
            return Ok(_courseService.GetAllCourses());
        }

        [HttpGet("ProfessorCourses")]
        public GlobalResponseDTO GetManagedCourses(string professor_id)
        {
            return _courseService.GetManagedCourses(professor_id);
        }

        [HttpGet("StudentCourses")]
        public GlobalResponseDTO GetStudentCourses(string student_id)
        {
            return _courseService.GetStudentCourses(student_id);
        }
    }
}
