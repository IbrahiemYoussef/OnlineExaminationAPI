using AutoMapper;
using FinalYearProject.Models;
using FinalYearProject.Models.DTOs;
using FinalYearProject.Services;
using Microsoft.AspNetCore.Cors;
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
    [EnableCors]
    public class ScheduleController : ControllerBase
    {
        //private ScheduleService _ScheduleService;
        //public ScheduleController(ScheduleService scheduleservice)
        //{
        //    _ScheduleService = scheduleservice;
        //},ScheduleService servicee  _ScheduleService = servicee;
        //[Authorize(Roles = UserRoles.Admin)]
        private mydbcon _context;
        //private ScheduleService _ScheduleService;
        private readonly IMapper _mapper;
        private ScheduleService _ScheduleService;
        public ScheduleController(mydbcon context, IMapper mapper, ScheduleService scheduleService)
        {
            _context = context;
            _mapper = mapper;
            _ScheduleService = scheduleService;
        }
        [HttpPost]
        [Route("CreateSchedule")]
        public GlobalResponseDTO CreateSchedule(string startdatee)
        {
            return _ScheduleService.CreateSchedule(startdatee);
        }
    
        [HttpGet]
        [Route("GetFacultySchedule")]
        public GlobalResponseDTO GetSchedule()
        {
            return _ScheduleService.GetSchedule();
        }

        [HttpDelete]
        [Route("DeleteSchedule")]
        public GlobalResponseDTO DelSchdeule()
        {
            return _ScheduleService.deletSchedule();
        }


        //[HttpPost]
        //[Route("CreateScheduleeeee")]
        //public GlobalResponseDTO CreateScheduleee(DateTime startdate)
        //{
        //    return _ScheduleService.CreateSchedule(startdate);
        //}
    }
}
