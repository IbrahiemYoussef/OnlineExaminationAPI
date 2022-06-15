using FinalYearProject.Models;
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
        [Authorize(Roles = UserRoles.Professor)]
        [HttpPost("PostExamDetails")]
        public GlobalResponseDTO AddExamDetails(int course_id, [FromBody] ExamDetailsDTO examdto)
        {
            return _examService.AddExamDetails(course_id, examdto);
        }
        [Authorize(Roles = UserRoles.Professor)]
        [HttpPut("PutExamDetails")]
        public GlobalResponseDTO UpdateExamByCourseId(int course_id, [FromBody] UpdateExamDetailsDTO examdto)
        {
            return _examService.UpdateExamDetails(course_id, examdto);
        }
        [Authorize(Roles = UserRoles.Professor)]
        [HttpDelete("DeleteExamDetails")]
        public GlobalResponseDTO DeleteExamDetails(int course_id)
        {
            return _examService.DeleteExamDetails(course_id);

        }
        [Authorize(Roles = UserRoles.Professor)]
        [HttpGet("GetExamDetails")]
        public GlobalResponseDTO GetExamBycourseId(int course_id)
        {
            return _examService.GetExamdetailsByCourseId(course_id);
        }
        [Authorize(Roles = UserRoles.Student)]
        [HttpGet("Examinate")]
        public IActionResult ExaminateG(string student_id, int course_id)
        {
            return Ok(_examService.GetUniqueExam(student_id, course_id));
        }
        [Authorize(Roles = UserRoles.Student)]
        [HttpPost("Examinate")]
        public IActionResult ExaminateP([FromBody] ExaminateDTO obj)
        {
            return Ok(_examService.GetExamResult(obj.student_id, obj.course_id, obj.total_num_of_questions, obj.answers));
        }

    }
}
