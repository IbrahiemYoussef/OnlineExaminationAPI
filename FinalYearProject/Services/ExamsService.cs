using AutoMapper;
using FinalYearProject.Models;
using FinalYearProject.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public void AddExamDetails(ExamDetailsDTO examdetail)
        {
            
                var _examdetaill = new ExamDetails()
                {
                    NumberOfQuestions = examdetail.NumberOfQuestions,
                    NumberOfEasyQuestions = examdetail.NumberOfEasyQuestions,
                    NumberOfModQuestions = examdetail.NumberOfModQuestions,
                    NumberOfHardQuestions = examdetail.NumberOfHardQuestions,
                    TypeOfQuestions=examdetail.TypeOfQuestions,
                    Course_id = examdetail.Course_id

                };
                if (_examdetaill.NumberOfEasyQuestions + _examdetaill.NumberOfModQuestions + _examdetaill.NumberOfHardQuestions == _examdetaill.NumberOfQuestions)
                {
                    _context.ExamDetails.Add(_examdetaill);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("please enter the right details");
                }
            
            
        }


        public void DeleteExamDetails(int id)
        {
            var examdetail = new ExamDetails();
            examdetail = _context.ExamDetails.FirstOrDefault(x => x.Course_id == id);
            _context.ExamDetails.Remove(examdetail);
            _context.SaveChanges();
        }
        public ExamDetails UpdateExamDetails(int id, ExamDetailsDTO examdetail)
        {
            var examdetaill = new ExamDetails();
            examdetaill = _context.ExamDetails.FirstOrDefault(x => x.Course_id == id);
            if (examdetaill != null)
            {
                examdetaill.NumberOfQuestions = examdetail.NumberOfQuestions;
                examdetaill.NumberOfEasyQuestions = examdetail.NumberOfEasyQuestions;
                examdetaill.NumberOfModQuestions = examdetail.NumberOfModQuestions;
                examdetaill.NumberOfHardQuestions = examdetail.NumberOfHardQuestions;
                examdetaill.TypeOfQuestions = examdetail.TypeOfQuestions;
                if (examdetaill.NumberOfEasyQuestions + examdetaill.NumberOfModQuestions + examdetaill.NumberOfHardQuestions == examdetaill.NumberOfQuestions)
                {
                    
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("please enter the right details");
                }
            }
            return examdetaill;
        }

        public ExamDetails GetExamdetailsByCourseId(int course_id)
        {
            return _context.ExamDetails.FirstOrDefault(x => x.Course_id == course_id);
        }





        public List<QuestionDTO> GetUniqueExam(int coursee_id)
        {
            //if n > nrows in the table

            //get examdetails by course id
            ExamDetails examm = _context.ExamDetails.FirstOrDefault(x => x.Course_id == coursee_id);
            var n = examm.NumberOfQuestions;
            var neasy = examm.NumberOfEasyQuestions;
            var nmod = examm.NumberOfModQuestions;
            var nhard = examm.NumberOfHardQuestions;
            var type = examm.TypeOfQuestions;


            List<Question> questions;

            if (type.ToUpper() == "MCQ")
            {
                questions = _context.Questions.Where(x => x.CourseId == coursee_id && x.Goal != null).ToList();
                List<Question> easy_questions = questions.Where(x => x.Difficulty == "Easy").OrderBy(t => Guid.NewGuid()).Take(neasy).ToList();
                List<Question> moderate_questions = questions.Where(x => x.Difficulty == "Moderate").OrderBy(t => Guid.NewGuid()).Take(nmod).ToList();
                List<Question> hard_questions = questions.Where(x => x.Difficulty == "Hard").OrderBy(t => Guid.NewGuid()).Take(nhard).ToList();

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

            return _mapper.Map<List<QuestionDTO>>(questions);

        }

        public ResultDTO GetExamResult(int coursee_id,int? std_id, List<AnswerDTO>? answers)
        {
            var counter = 0;
            foreach (var ans in answers)
            {
              Question que=  _context.Questions.Where(x => x.CourseId == coursee_id).
                    Where(x => x.Id == ans.Id && x.Goal == ans.Answer).FirstOrDefault();
                if (que != null)
                    counter++;

            }
            ResultDTO r = new ResultDTO()
            {
                CurrentScore = counter,
                TotalScore = answers.Count()
            };
            return r;
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
