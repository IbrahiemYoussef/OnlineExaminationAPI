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
        [HttpPost]
        public GlobalResponseDTO AddExamDetails( [FromBody] ExamDetailsDTO examdto)
        {
           return _examService.AddExamDetails(examdto);
           
        }
        [HttpDelete]
        public GlobalResponseDTO DeleteExamDetails(int Course_id)
        {
          return  _examService.DeleteExamDetails(Course_id);
             
        }
        [HttpPut]
        public GlobalResponseDTO UpdateExamByCourseId(int Course_id, [FromBody] ExamDetailsDTO examdto)
        {
            return _examService.UpdateExamDetails(Course_id, examdto);


        }
        [HttpGet]
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


        //[HttpGet("SubmittingExam"), HttpPost("SubmittingExam")]
        ////public IActionResult SubmittingExam(int std_id,int coursee_id,List<AnswerDTO>? answers)
        ////{
        ////    Examinate(coursee_id);
        ////    return Ok(_examService.GetExamResult(coursee_id, std_id, answers));

        ////}
    }
}
