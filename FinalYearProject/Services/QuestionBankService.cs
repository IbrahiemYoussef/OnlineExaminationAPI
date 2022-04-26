using CsvHelper;
using CsvHelper.Configuration;
using FinalYearProject.Models;
using FinalYearProject.Models.DTOs;
using FinalYearProject.Models.Pojo;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FinalYearProject.Services
{

    public class QuestionBankService
    {
        private mydbcon _context;
        public QuestionBankService(mydbcon context)
        {
            _context = context;
        }

        public GlobalResponseDTO GetQuestionsById(int course_id)
        {
            int count = _context.Questions.Where(q => q.CourseId == course_id).Count();
            return new GlobalResponseDTO(true,"Fetched number of records succesfully",new { records_count= count });
        }

        public GlobalResponseDTO DeleteQuestionsById(int course_id)
        {
            _context.Questions.RemoveRange(_context.Questions.Where(q => q.CourseId == course_id));
            ExamDetails examdetail = _context.ExamDetails.Where(x => x.Course_id == course_id).FirstOrDefault();
            examdetail.isQuestionBankConfigured = false;
            _context.SaveChanges();
            return new GlobalResponseDTO(true, "Succesfully erased the question bank",null );
        }

        public GlobalResponseDTO UploadFile(IFormFile file, string QuestionType, int course_id)
        {

            var records_count = 0;
            //client form + params
            try
            {
                string result = "";
                result += course_id.ToString() + "\n";
                result += QuestionType.ToString() + "\n";



            //if (!isCsvFile(file)) return new GlobalResponseDTO(false, "Invalid File type", null);

                if (_context.Courses.Find(course_id) == null)
                    return new GlobalResponseDTO(false, "Invalid Course id", null);

                ExamDetails exam_details_obj = _context.ExamDetails.Where(e => e.Course_id == course_id).FirstOrDefault();
                if (exam_details_obj == null)
                    return new GlobalResponseDTO(false, "You have to set the exam details before uploading the question bank", null);

               
                if (QuestionType.ToUpper()[0].ToString() == "M")
                {
                    using (var reader = new StreamReader(file.OpenReadStream()))
                    using (var csvReader = new CsvReader(reader, CultureInfo.CurrentCulture))
                    {
                        var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture) { HasHeaderRecord = true };
                        IEnumerable<MCQquestions> csvRecords = csvReader.GetRecords<MCQquestions>();
                        //records_count = csvRecords.Count();
                        foreach (var s in csvRecords)
                        {

                            if (s.goal.Length == 1 && s.c != "" && s.d != "")
                            {
                                var questionnn = new Question()
                                {
                                    Questionx = s.question,
                                    Qtype = 'M',
                                    A = s.a,
                                    B = s.b,
                                    C = s.c,
                                    D = s.d,
                                    Goal = s.goal,
                                    Difficulty = s.difficulty.ToUpper(),
                                    CourseId = course_id
                                };
                                _context.Questions.Add(questionnn);
                            }
                            else if (s.goal.Length > 1)
                            {
                                var questionnn = new Question()
                                {
                                    Questionx = s.question,
                                    Qtype = 'Y',
                                    A = s.a,
                                    B = s.b,
                                    C = s.c,
                                    D = s.d,
                                    Goal = String.Concat(s.goal.Replace(",", "").OrderBy(c => c)),
                                    Difficulty = s.difficulty.ToUpper(),
                                    CourseId = course_id
                                };
                                _context.Questions.Add(questionnn);
                            }
                            else
                            {
                                var questionnn = new Question()
                                {
                                    Questionx = s.question,
                                    Qtype = 'T',
                                    A = s.a,
                                    B = s.b,
                                    C = s.c,
                                    D = s.d,
                                    Goal = s.goal,
                                    Difficulty = s.difficulty.ToUpper(),
                                    CourseId = course_id
                                };
                                _context.Questions.Add(questionnn);
                            }

                            result += s.question + "\n";
                            records_count++;
                        }

                        
                    }
                }
                else if (QuestionType.ToUpper()[0].ToString() == "W")
                {
                    using (var reader = new StreamReader(file.OpenReadStream()))
                    using (var csvReader = new CsvReader(reader, CultureInfo.CurrentCulture))
                    {
                        var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture) { HasHeaderRecord = true };
                        IEnumerable<WrittenQ> csvRecords = csvReader.GetRecords<WrittenQ>();
                        //records_count = csvRecords.Count();
                        foreach (var s in csvRecords)
                        {
                            var questionnn = new Question()
                            {
                                Questionx = s.question,
                                Qtype = 'W',
                                Answer = s.answer,
                                Difficulty = s.difficulty.ToUpper(),
                                CourseId = course_id
                            };
                            _context.Questions.Add(questionnn);

                            result += s.question + "\n";
                            records_count++;
                        }

                        
                    }
                
                }
                else
                {
                    return new GlobalResponseDTO(true, "Invalid question bank type", result);
                }

                if (exam_details_obj.NumberOfQuestions > records_count)
                    return new GlobalResponseDTO(false, "Number of Question bank records are less than required in the exam details", new {res=result,count=records_count });

                exam_details_obj.isQuestionBankConfigured = true;

                _context.SaveChanges();
                return new GlobalResponseDTO(true, "Question bank inserted successfully", result);

            }
            catch (Exception ex)
            {
                return new GlobalResponseDTO(false, ex.Message, null);
            }
        }
        private bool isCsvFile(IFormFile file)
        {
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            return (extension == ".csv"); // Change the extension based on your need
        }

    }
}
