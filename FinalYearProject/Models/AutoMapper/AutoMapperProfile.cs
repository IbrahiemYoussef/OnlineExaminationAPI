using AutoMapper;
using FinalYearProject.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalYearProject.Models.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //source ,destination
            CreateMap<Course, CourseDTO >();

            CreateMap<Question, QuestionDTO>();

            CreateMap<ExamDetails, ExamDetailsDTO>();
    
        }
    }
}
