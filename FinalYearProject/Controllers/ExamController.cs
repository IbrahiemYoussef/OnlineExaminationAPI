﻿using FinalYearProject.Models;
using FinalYearProject.Models.DTOs;
using FinalYearProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using FinalYearProject.Models.Params;
using System.Threading.Tasks;

namespace FinalYearProject.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    
    public class ExamController : ControllerBase
    {
        private ExamsService _examService;
        public ExamController(ExamsService examService)
        {
            _examService = examService;
        }
        [HttpPost("PostExamDetails")]
        public GlobalResponseDTO AddExamDetails(int course_id,[FromBody] ExamDetailsDTO examdto)
        {
           return _examService.AddExamDetails(course_id,examdto);
        }

        [HttpPut("PutExamDetails")]
        public GlobalResponseDTO UpdateExamByCourseId(int course_id, [FromBody] UpdateExamDetailsDTO examdto)
        {
            return _examService.UpdateExamDetails(course_id, examdto);
        }

        [HttpDelete("DeleteExamDetails")]
        public GlobalResponseDTO DeleteExamDetails(int course_id)
        {
          return  _examService.DeleteExamDetails(course_id);
             
        }

        [HttpGet("GetExamDetails")]
        public GlobalResponseDTO GetExamBycourseId(int course_id)
        {
            return _examService.GetExamdetailsByCourseId(course_id);
        }

        [HttpGet("GetMyExam")]
        public IActionResult GetMyExam(int course_id)
        {
            return Ok(_examService.GetUniqueExam(course_id));
        }

        [HttpGet("Examinate")]
        public IActionResult ExaminateG(int course_id)
        {
            return Ok(_examService.GetUniqueExam(course_id));
        }

        [HttpPost("Examinate")]
        public IActionResult ExaminateP([FromBody] ExaminateDTO obj)
        {
             return Ok(_examService.GetExamResult(obj.std_id,obj.course_id,obj.answers));
        }

    }
}
