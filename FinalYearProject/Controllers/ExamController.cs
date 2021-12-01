using FinalYearProject.Models;
using FinalYearProject.Models.ResponseModels;
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
    public class ExamController : ControllerBase
    {
        private ExamsService _examService;
        public ExamController(ExamsService examService)
        {
            _examService = examService;
        }

        [HttpGet]

        public List<ExaminationQuestion> GetUniqueExam(string course_name,int n,int neasy,int nmod,int nhard,string type)
        {
             return _examService.GetUniqueExam(course_name, n,neasy, nmod, nhard, type);
        }
    }
}
