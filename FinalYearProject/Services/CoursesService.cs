using AutoMapper;
using FinalYearProject.Models;
using FinalYearProject.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalYearProject.Services
{
    public class CoursesService
    {
        private mydbcon _context;
        private readonly IMapper _mapper;
        public CoursesService(mydbcon context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<CourseDTO> GetManagedCourses(string prof_id)
        {
            var res= _context.Courses.Where(x => x.ApplicationUserId == prof_id).ToList();
            return _mapper.Map<List<CourseDTO>>(res);

        }
    }
}
