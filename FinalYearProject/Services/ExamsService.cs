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
        public GlobalResponseDTO AddExamDetails(ExamDetailsDTO examdetail)
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
                    Course_id = examdetail.Course_id

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


        public GlobalResponseDTO DeleteExamDetails(int id)
        {
            var examdetail = new ExamDetails();
            examdetail = _context.ExamDetails.FirstOrDefault(x => x.Course_id == id);
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
        public GlobalResponseDTO UpdateExamDetails(int id, UpdateExamDetailsDTO examdetail)
        {
            var examdetaill = new ExamDetails();
            examdetaill = _context.ExamDetails.FirstOrDefault(x => x.Course_id == id);
            if (examdetaill != null)
            {
                examdetaill.NumberOfQuestions = examdetail.NumberOfQuestions;
                //NumberOfMultipleMCQ = examdetail.NumberOfMultipleMCQ,
                //    NumberOfSingleMCQ = examdetail.NumberOfSingleMCQ,
                //    NumberOfTrueFalse = examdetail.NumberOfTrueFalse,
                //    NumberOfWritten = examdetail.NumberOfWritten,
                examdetaill.NumberOfEasyQuestions = examdetail.NumberOfEasyQuestions;
                examdetaill.NumberOfModQuestions = examdetail.NumberOfModQuestions;
                examdetaill.NumberOfHardQuestions = examdetail.NumberOfHardQuestions;
                if (examdetaill.NumberOfEasyQuestions + examdetaill.NumberOfModQuestions + examdetaill.NumberOfHardQuestions == examdetaill.NumberOfQuestions)
                {
                    
                    _context.SaveChanges();
                    return new GlobalResponseDTO(true, "succeeded", examdetaill);
                }
                else
                {
                    return new GlobalResponseDTO(false, "Failed", "Wrong data entry");
                }
            }
            return new GlobalResponseDTO(false, "failed", "Not exist");
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





        public GlobalResponseDTO GetUniqueExam(int coursee_id)
        {
            //seyahak tarab
            //ehda ela Ibrahiem Yousef
            //3lk IT & CS

            //if n > nrows in the table

            //get examdetails by course id

            //handle wrong course id

            ExamDetails examm = _context.ExamDetails.FirstOrDefault(x => x.Course_id == coursee_id);
            if(examm==null)
                return new GlobalResponseDTO(false, "Invalid CourseID", null);

            var n = examm.NumberOfQuestions;
            var neasy = examm.NumberOfEasyQuestions;
            var nmod = examm.NumberOfModQuestions;
            var nhard = examm.NumberOfHardQuestions;
            var type = "";//examm.TypeOfQuestions;


            List<Question> questions;

            if (type.ToUpper() == "MCQ")
            {
                questions = _context.Questions.Where(x => x.CourseId == coursee_id && x.Goal != null).ToList();
                List<Question> easy_questions = questions.Where(x => x.Difficulty.ToUpper() == "EASY").OrderBy(t => Guid.NewGuid()).Take(neasy).ToList();
                List<Question> moderate_questions = questions.Where(x => x.Difficulty.ToUpper() == "MODERATE").OrderBy(t => Guid.NewGuid()).Take(nmod).ToList();
                List<Question> hard_questions = questions.Where(x => x.Difficulty.ToUpper() == "HARD").OrderBy(t => Guid.NewGuid()).Take(nhard).ToList();

                List<Question> result;
                result = easy_questions.Concat(moderate_questions).ToList();
                result = result.Concat(hard_questions).OrderBy(x => Guid.NewGuid()).ToList(); //shuffle overall

                questions = result;
            }
            else
            {
                //if exam is mix 20% written 
                int numofwr = Convert.ToInt32(Math.Ceiling(.2 * n));
                int numofmcq = n - numofwr;

                //questions = _context.Questions.Where(x => x.CourseId == coursee_id).ToList();
                List<Question> wr_ques = _context.Questions.Where(x => x.CourseId == coursee_id && x.Goal == null).OrderBy(t => Guid.NewGuid()).Take(numofwr).ToList();
                List<Question> mcq_ques = _context.Questions.Where(x => x.CourseId == coursee_id && x.Goal != null).OrderBy(t => Guid.NewGuid()).Take(numofmcq).ToList();
                questions = wr_ques.Concat(mcq_ques).ToList();
            }
            //time of exam in seconds 


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

        public GlobalResponseDTO GetExamResult(string std_id,int coursee_id, List<AnswerDTO> answers)
        {
            var mcq_counter = 0;
            List<string> user_answers = answers.Where(x=> x.qtype.ToString().ToLower() == "w")
                          .Select(a => a.Answer).ToList();

            List<string> model_answers= new List<string>();
            
            foreach (AnswerDTO ans in answers)
            {
                if (ans.qtype.ToString().ToLower() == "w")
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

            // Console.WriteLine(sc.Result.score);
            //Console.WriteLine(sc.Result.total_num_of_ques);

                ResultDTO robj = new ResultDTO()
                {
                    CurrentScore = mcq_counter + sc.Result.score,
                    TotalScore = answers.Count()
             
                };
            
            Enrollment std_enrol = new Enrollment();
            std_enrol = _context.Enrollments.Where(x => x.ApplicationUserId == std_id && x.CourseId == coursee_id).FirstOrDefault();
            std_enrol.CurrentMarks = robj.CurrentScore;
            std_enrol.TotalMarks = robj.TotalScore;
            _context.SaveChanges();
            


            return new GlobalResponseDTO(true, "Exam Sucessfully Graded", robj);
        }

    }
    

    //public List<QuestionDTO> GetUniqueExam(string course_name, int n,int neasy, int nmod, int nhard, string type)
    //{
    //    //if n > nrows in the table

    //    //get course id by name
    //    Course course = _context.Courses.FirstOrDefault(x => x.Name == course_name);
    //    List<Question> questions;

    //    if (type.ToUpper() == "MCQ")
    //    {
    //        questions = _context.Questions.Where(x => x.CourseId == course.Id && x.Goal != null).ToList();
    //        List<Question> easy_questions = questions.Where(x => x.Difficulty == "Easy").OrderBy(t => Guid.NewGuid()).Take(neasy).ToList();
    //        List<Question> moderate_questions = questions.Where(x => x.Difficulty == "Moderate").OrderBy(t => Guid.NewGuid()).Take(nmod).ToList();
    //        List<Question> hard_questions = questions.Where(x => x.Difficulty == "Hard").OrderBy(t => Guid.NewGuid()).Take(nhard).ToList();

    //        List<Question> result;
    //        result = easy_questions.Concat(moderate_questions).ToList();
    //        result = result.Concat(hard_questions).OrderBy(x => Guid.NewGuid()).ToList(); //shuffle overall

    //        questions = result;
    //    }
    //    else
    //    {
    //        //if exam is mix 20% written 
    //        int numofwr = Convert.ToInt32(Math.Ceiling(.2 * n));
    //        int numofmcq = n - numofwr;

    //        questions = _context.Questions.Where(x => x.CourseId == course.Id).ToList();
    //        List<Question> wr_ques = questions.Where(x => x.Goal == null).OrderBy(t => Guid.NewGuid()).Take(numofwr).ToList();
    //        List<Question> mcq_ques =   questions.Where(x => x.Goal != null).OrderBy(t => Guid.NewGuid()).Take(numofmcq).ToList();
    //        questions = wr_ques.Concat(mcq_ques).ToList();

    //    }

    //    return _mapper.Map<List<QuestionDTO>>(questions);

    //}


}
