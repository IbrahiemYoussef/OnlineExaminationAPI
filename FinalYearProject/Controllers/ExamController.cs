using FinalYearProject.Models;
using FinalYearProject.Models.DTOs;
using FinalYearProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FinalYearProject.Controllers
{
    [Authorize]
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
        public IActionResult AddExamDetails( [FromBody] ExamDetailsDTO examdto)
        {
            _examService.AddExamDetails(examdto);
            return Ok();
        }
        [HttpDelete]
        public IActionResult DeleteExamDetails(int Course_id)
        {
            _examService.DeleteExamDetails(Course_id);
            return Ok();
        }
        [HttpPut]
        public IActionResult UpdateExamByCourseId(int Course_id, [FromBody] ExamDetailsDTO examdto)
        {
            return Ok(_examService.UpdateExamDetails(Course_id, examdto));


        }
        [HttpGet]
        public IActionResult GetExamBycourseId(int course_id)
        {
            return Ok(_examService.GetExamdetailsByCourseId(course_id));
        }

        [HttpPost("Examinate")]
        public IActionResult Examinate(int? std_id, int coursee_id, [FromBody]List<AnswerDTO> answers=null)
        {
            if (std_id == null)
                return Ok(_examService.GetUniqueExam(coursee_id));
            else
                return Ok(_examService.GetExamResult(coursee_id, std_id, answers));
        }



        //[HttpGet("SubmittingExam"), HttpPost("SubmittingExam")]
        ////public IActionResult SubmittingExam(int std_id,int coursee_id,List<AnswerDTO>? answers)
        ////{
        ////    Examinate(coursee_id);
        ////    return Ok(_examService.GetExamResult(coursee_id, std_id, answers));

        ////}
    }
}
