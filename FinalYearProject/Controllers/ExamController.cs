using FinalYearProject.Models.DTOs;
using FinalYearProject.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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

        [HttpGet]

        public List<QuestionDTO> GetUniqueExam(string course_name,int n,int neasy,int nmod,int nhard,string type)
        {
             return _examService.GetUniqueExam(course_name, n,neasy, nmod, nhard, type);
        }
    }
}
