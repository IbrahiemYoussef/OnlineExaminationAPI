using AutoMapper;
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
    public class CourseController : ControllerBase
    {
        private CoursesService _courseService;
        public CourseController(CoursesService courseService)
        {
            _courseService = courseService;
            
        }

        [HttpGet("{prof_id}")]
        public IActionResult GetManagedCourses(string prof_id)
        {
            return Ok(_courseService.GetManagedCourses(prof_id));
        }

        [HttpGet("StudentCourses")]
        public GlobalResponseDTO GetStudentCourses(string std_id)
        {
            return _courseService.GetStudentCourses(std_id);
        }
    }
}
