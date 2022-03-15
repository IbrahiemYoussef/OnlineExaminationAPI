using FinalYearProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalYearProject.Services
{
    public class ScheduleService
    {
        private mydbcon _context;
        
        public ScheduleService(mydbcon context)
        {
            _context = context;
            
        }
        //public List<Schedule> CreateSchedule()
        //{

        //}
    }
}
