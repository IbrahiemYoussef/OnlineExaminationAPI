using FinalYearProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using FinalYearProject.Services;
using FinalYearProject.Models.DTOs;

namespace FinalYearProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionBankController : ControllerBase
    {
        private QuestionBankService _questionBankService;
        public QuestionBankController(QuestionBankService questionBankService)
        {
            _questionBankService = questionBankService; 
        }

        [HttpPost("UploadQuestionBank")]
        public IActionResult UploadFile(IFormFile file ,string questionBankType,int course_id)
        {

            return Ok(_questionBankService.UploadFile(file, questionBankType, course_id));

        }

        [HttpGet("GetQuestionsById")]
        public IActionResult GetQuestionsById(int course_id)
        {

            return Ok(_questionBankService.GetQuestionsById(course_id));

        }

        [HttpDelete("DeleteQuestionsById")]
        public IActionResult DeleteQuestionsById(int course_id)
        {

            return Ok(_questionBankService.DeleteQuestionsById(course_id));

        }


    }
}
