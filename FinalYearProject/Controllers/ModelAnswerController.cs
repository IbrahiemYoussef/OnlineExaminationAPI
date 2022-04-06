using CsvHelper;
using CsvHelper.Configuration;
using FinalYearProject.Models;
using FinalYearProject.Models.Pojo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using FinalYearProject.Models.DTOs;

namespace FinalYearProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class ModelAnswerController : ControllerBase
    {
        private readonly mydbcon _context;
        public ModelAnswerController(mydbcon context)
        {
            _context = context;
            
        }


        [HttpPost]
        public IActionResult UploadFile(IFormFile file ,string QuestionType,int CourseIdd)
        {
            string result = "";
            result += CourseIdd.ToString() + "\n";
            result += QuestionType.ToString() + "\n";
            if (QuestionType.ToUpper()[0].ToString() == "M")
            {           
                using (var reader = new StreamReader(file.OpenReadStream()))
                using (var csvReader = new CsvReader(reader, CultureInfo.CurrentCulture))
                {
                    var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
                    {
                        HasHeaderRecord = true
                    };
                    var MCQques = csvReader.GetRecords<MCQquestions>();
                    foreach (var s in MCQques)
                    {
                            
                        if (s.goal.Length == 1 && s.c != ""  && s.d !="" )
                        { 
                            var questionnn = new Question()
                            {
                            Questionx = s.question,
                            Qtype = 'M',
                            A = s.a,
                            B = s.b,
                            C=s.c,
                            D=s.d,
                            Goal = s.goal,
                            Difficulty = s.difficulty,
                            CourseId=CourseIdd
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
                                Difficulty = s.difficulty,
                                CourseId = CourseIdd
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
                                Difficulty = s.difficulty,
                                CourseId = CourseIdd
                            };
                            _context.Questions.Add(questionnn);
                        }

                        /*need to change cause this is taking time while using*/
                        result += s.question + "\n";
                    }
                    _context.SaveChanges();
                }
                return Ok(new GlobalResponseDTO(true,"MCQ Added Sucessfully",result));
                
            }
            else if (QuestionType.ToUpper()[0].ToString() == "W")
            {   
                using (var reader = new StreamReader(file.OpenReadStream()))
                using (var csvReader = new CsvReader(reader, CultureInfo.CurrentCulture))
                {
                    var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
                    {
                        HasHeaderRecord = true
                    };
                    var sheet = csvReader.GetRecords<WrittenQ>();

                    foreach (var s in sheet)
                    {
                        var questionnn = new Question()
                        {
                            Questionx = s.question,
                            Qtype = 'W',
                            Answer=s.answer,
                            Hint=s.Hint,
                            Difficulty = s.difficulty,
                            CourseId = CourseIdd
                        };
                            _context.Questions.Add(questionnn);

                        result += s.question + "\n";
                    }
                    _context.SaveChanges();
                }
                return Ok(new GlobalResponseDTO(true, "Written Questions Added Sucessfully", result));   
            }
            else
            {
                return Ok(new GlobalResponseDTO(true, "Please provide a valid file type", result));
            }

        }

        private bool CheckIfExcelFile(IFormFile file)
        {
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            return (extension == ".xlsx" || extension == ".xls" || extension == ".csv"); // Change the extension based on your need
        }
    }
}
