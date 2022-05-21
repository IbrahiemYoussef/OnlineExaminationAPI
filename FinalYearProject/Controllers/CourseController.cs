using AutoMapper;
using FinalYearProject.Models;
using FinalYearProject.Models.DTOs;
using FinalYearProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;

namespace FinalYearProject.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private CoursesService _courseService;
        public CourseController(CoursesService courseService)
        {
            _courseService = courseService;
            
        }
        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("GetAllCourses")]
        public IActionResult GetAllCourses()
        {
            return Ok(_courseService.GetAllCourses());
        }
        [Authorize(Roles = UserRoles.Professor)]
        [HttpGet("ProfessorCourses")]
        public GlobalResponseDTO GetProfessorCourses(string professor_id)
        {
            return _courseService.GetProfessorCourses(professor_id);
        }
        [Authorize(Roles = UserRoles.Student)]
        [HttpGet("StudentCourses")]
        public GlobalResponseDTO GetStudentCourses(string student_id)
        {
            return _courseService.GetStudentCourses(student_id);
        }
    }
}
