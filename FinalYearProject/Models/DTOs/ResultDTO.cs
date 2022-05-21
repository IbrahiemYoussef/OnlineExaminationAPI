using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalYearProject.Models.DTOs
{
    public class ResultDTO
    {
        public int Id { get; set; }
        public int CurrentScore { get; set; }
        public int TotalScore { get; set; }

        public string Grade { get; set; }
    }
}
