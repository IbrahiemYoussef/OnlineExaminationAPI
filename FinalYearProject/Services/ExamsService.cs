using AutoMapper;
using FinalYearProject.Models;
using FinalYearProject.Models.DTOs;
using FinalYearProject.Models.Pojo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace FinalYearProject.Services
{

    public class ExamsService
    {
        //hemajoo-001 hosting id
        private mydbcon _context;
        private readonly IMapper _mapper;
        public ExamsService(mydbcon context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public GlobalResponseDTO AddExamDetails(int course_id,ExamDetailsDTO examdetail)
        {
            
                var _examdetaill = new ExamDetails()
                {
                    NumberOfQuestions = examdetail.NumberOfQuestions,
                    //NumberOfMultipleMCQ=examdetail.NumberOfMultipleMCQ,
                    //NumberOfSingleMCQ=examdetail.NumberOfSingleMCQ,
                    //NumberOfTrueFalse=examdetail.NumberOfTrueFalse,
                    //NumberOfWritten=examdetail.NumberOfWritten,
                    NumberOfEasyQuestions = examdetail.NumberOfEasyQuestions,
                    NumberOfModQuestions = examdetail.NumberOfModQuestions,
                    NumberOfHardQuestions = examdetail.NumberOfHardQuestions,
                    TypeOfQuestions = examdetail.TypeOfQuestions,
                    Course_id = course_id
                };
                if (_examdetaill.NumberOfEasyQuestions + _examdetaill.NumberOfModQuestions + _examdetaill.NumberOfHardQuestions == _examdetaill.NumberOfQuestions)
                {
                    _context.ExamDetails.Add(_examdetaill);
                    _context.SaveChanges();
                    return  new GlobalResponseDTO(true, "Succeeded", _examdetaill);
                }
                else
                {
                    return new GlobalResponseDTO(false, "Failed wrong data entry", null);
                }
            
            
        }

        public GlobalResponseDTO DeleteExamDetails(int course_id)
        {
            var examdetail = new ExamDetails();
            examdetail = _context.ExamDetails.FirstOrDefault(x => x.Course_id == course_id);
            if (examdetail != null)
            {
                _context.ExamDetails.Remove(examdetail);
                _context.SaveChanges();
                return new GlobalResponseDTO(true, "succeeded", "Deleted Successfully");
            }
            else
            {
                return new GlobalResponseDTO(false, "Failed", "this course doesn't have a exam details");
            }
            
        }
        public GlobalResponseDTO UpdateExamDetails(int course_id, UpdateExamDetailsDTO examdetail)
        {
            var examdetaill = new ExamDetails();
            examdetaill = _context.ExamDetails.FirstOrDefault(x => x.Course_id == course_id);
            if (examdetaill != null)
                return new GlobalResponseDTO(false, "Failed invalid course id",null );

            examdetaill.NumberOfQuestions = examdetail.NumberOfQuestions;
            examdetaill.NumberOfEasyQuestions = examdetail.NumberOfEasyQuestions;
            examdetaill.NumberOfModQuestions = examdetail.NumberOfModQuestions;
            examdetaill.NumberOfHardQuestions = examdetail.NumberOfHardQuestions;
            examdetaill.TypeOfQuestions = examdetail.TypeOfQuestions; //mcq or mixed
            if (examdetaill.NumberOfEasyQuestions + examdetaill.NumberOfModQuestions + examdetaill.NumberOfHardQuestions == examdetaill.NumberOfQuestions)
            {
                _context.SaveChanges();
                return new GlobalResponseDTO(true, "Succeeded", examdetaill);
            }
            else
            {
                return new GlobalResponseDTO(false, "Wrong data entry!", null);
            }
            
            
        }

        public GlobalResponseDTO GetExamdetailsByCourseId(int course_id)
        {
            var examdetail= _context.ExamDetails.FirstOrDefault(x => x.Course_id == course_id);
            if (examdetail != null)
            {
                return new GlobalResponseDTO(true, "succeeded", examdetail);
            }
            else
            {
                return new GlobalResponseDTO(false, "Failed", "this course doesn't have a exam details");
            }
        }

        public GlobalResponseDTO GetUniqueExam(string student_id,int coursee_id)
        {

            Enrollment enrollment = _context.Enrollments.Where(e => e.CourseId == coursee_id && e.ApplicationUserId == student_id).FirstOrDefault();
            if(enrollment==null)
                return new GlobalResponseDTO(false, "Student is not enrolled to this course", null);

            if(enrollment.isExaminated)
                return new GlobalResponseDTO(false, "Student can't retake this exam", null);

            ExamDetails examm = _context.ExamDetails.FirstOrDefault(x => x.Course_id == coursee_id);
            if(examm==null)
                return new GlobalResponseDTO(false, "Invalid Course ID", null);

            if(examm.NumberOfQuestions > _context.Questions.Where(q=>q.CourseId==coursee_id).Count())
                return new GlobalResponseDTO(false, "Failed to create an exam, Question Bank is in shortage mode", null);

            

            var n = examm.NumberOfQuestions;
            var neasy = examm.NumberOfEasyQuestions;
            var nmod = examm.NumberOfModQuestions;
            var nhard = examm.NumberOfHardQuestions;
            var type = examm.TypeOfQuestions;

            IQueryable<Question> questions;

            if (type.ToUpper() == "MCQ")
            {
                questions = _context.Questions.Where(x => x.CourseId == coursee_id && x.Goal != null);
                IQueryable<Question> easy_questions = questions.Where(x => x.Difficulty.ToUpper() == "EASY").OrderBy(t => Guid.NewGuid()).Take(neasy);
                IQueryable<Question> moderate_questions = questions.Where(x => x.Difficulty.ToUpper() == "MODERATE").OrderBy(t => Guid.NewGuid()).Take(nmod);
                IQueryable<Question> hard_questions = questions.Where(x => x.Difficulty.ToUpper() == "HARD").OrderBy(t => Guid.NewGuid()).Take(nhard);

                IQueryable<Question> result;
                result = easy_questions.Concat(moderate_questions);
                result = result.Concat(hard_questions).OrderBy(x => Guid.NewGuid()); //shuffle overall

                questions = result;
            }
            else
            {
                //if exam is mix 20% written 
                int numofwr = Convert.ToInt32(Math.Ceiling(.2 * n));
                int numofmcq = n - numofwr;
                 
                IQueryable<Question> wr_ques = _context.Questions.Where(x => x.CourseId == coursee_id && x.Goal == null).OrderBy(t => Guid.NewGuid()).Take(numofwr);
                IQueryable<Question> mcq_ques = _context.Questions.Where(x => x.CourseId == coursee_id && x.Goal != null).OrderBy(t => Guid.NewGuid()).Take(numofmcq);
                questions = wr_ques.Concat(mcq_ques);
            }

            enrollment.isExaminated = true; //very important to lock him out
            _context.SaveChanges();

            var list= _mapper.Map<List<QuestionDTO>>(questions);
            return new GlobalResponseDTO(true, "Exam Generated", list);
        }

        public static async Task<ScoreDTO> get_written_result(string url, List<string> user_answersx, List<string> model_answersx)
        {
            HttpClient _httpClient = new HttpClient();
            JsonSerializerOptions _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var answerito_obj = new Answeito
            {
                user_answers = user_answersx,
                model_answers = model_answersx
            };
            string answerito_obj_serialized = JsonSerializer.Serialize(answerito_obj);
            StringContent requestContent = new StringContent(answerito_obj_serialized, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, requestContent);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var given_score = JsonSerializer.Deserialize<ScoreDTO>(content, _options);
            return given_score;
        }

        public GlobalResponseDTO GetExamResult(string std_id,int coursee_id,int total_num_of_questions, List<AnswerDTO> answers)
        {
            var mcq_counter = 0;
            List<string> user_answers = answers.Where(x=> x.Qtype.ToString().ToLower() == "w")
                          .Select(a => a.Answer).ToList();

            List<string> model_answers= new List<string>();
            
            foreach (AnswerDTO ans in answers)
            {
                if (ans.Qtype.ToString().ToLower() == "w")
                {
                    Question que = _context.Questions.Where(x => x.Id == ans.Id).FirstOrDefault();
                    model_answers.Add(que.Answer);
                }
                else
                {
                    var sorted_ans = String.Concat(ans.Answer.OrderBy(c => c));
                    Question que = _context.Questions.Where(x => x.CourseId == coursee_id).
                    Where(x => x.Id == ans.Id && x.Goal == sorted_ans).FirstOrDefault();
                        if (que != null)
                        mcq_counter++;
                }
            }


            string target_url = "http://127.0.0.1:5000/evaluate";
            Task<ScoreDTO> sc = get_written_result(target_url, user_answers, model_answers);


            int current_score = mcq_counter + sc.Result.score;

            ResultDTO robj = new ResultDTO()
            {
                CurrentScore = current_score,
                TotalScore = total_num_of_questions
             
            };
            
            Enrollment std_enrollment = new Enrollment();
            std_enrollment = _context.Enrollments.Where(x => x.ApplicationUserId == std_id && x.CourseId == coursee_id).FirstOrDefault();
            std_enrollment.CurrentMarks = robj.CurrentScore;
            std_enrollment.TotalMarks = robj.TotalScore;
            std_enrollment.Grade = getGrade(current_score, total_num_of_questions);
            _context.SaveChanges();
            


            return new GlobalResponseDTO(true, "Exam Sucessfully Graded", robj);
        }

        private string getGrade(int current_score,int total_score)
        {
            double score = (double) Math.Round(Convert.ToDecimal(current_score / total_score));
            if (score >= 95)
                return "A+";           
            else if (score >= 90)
                return "A";
            else if (score >= 85)
                return "A-";
            else if (score >= 80)
                return "B+";
            else if (score >= 75)
                return "B";
            else if (score >= 70)
                return "C+";
            else if (score >= 65)
                return "C";
            else if (score >= 60)
                return "D";
            else
                return "F";
            
        }
    }
    
}
