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
        private mydbcon _context;
        public ExamsService(mydbcon context)
        {
            _context = context;
        }

        public List<QuestionDTO> GetUniqueExam(string course_name, int n,int neasy, int nmod, int nhard, string type)
        {
            //if n > nrows in the table

            //get course id by name
            Course course = _context.Courses.FirstOrDefault(x => x.Name == course_name);
            List<Question> questions;

            if (type.ToUpper() == "MCQ")
            {
                questions = _context.Questions.Where(x => x.CourseId == course.Id && x.Goal != null).ToList();
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

                questions = _context.Questions.Where(x => x.CourseId == course.Id).ToList();
                List<Question> wr_ques = questions.Where(x => x.Goal == null).OrderBy(t => Guid.NewGuid()).Take(numofwr).ToList();
                List<Question> mcq_ques =   questions.Where(x => x.Goal != null).OrderBy(t => Guid.NewGuid()).Take(numofmcq).ToList();
                questions = wr_ques.Concat(mcq_ques).ToList();

            }

            List<QuestionDTO>  examinationQuestions = new List<QuestionDTO>( new QuestionDTO[questions.Count] );

            for(int i=0;i< questions.Count; i++)
            {
                examinationQuestions[i] = new QuestionDTO();

                examinationQuestions[i].Id = questions[i].Id;
                examinationQuestions[i].Questionx = questions[i].Questionx;
                examinationQuestions[i].Answer = questions[i].Answer;
                if (questions[i].Goal == null)
                    examinationQuestions[i].Answer = null;
                examinationQuestions[i].Hint = questions[i].Hint;
                examinationQuestions[i].CourseId = questions[i].CourseId;
            }


            return examinationQuestions;

        }
    }
}
