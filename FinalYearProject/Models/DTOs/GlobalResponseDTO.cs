using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalYearProject.Models.DTOs
{
    public class GlobalResponseDTO
    {
        public GlobalResponseDTO(bool status, string message,object data)
        {
            this.status = status;
            this.message = message;
            this.data = data;
        }
        public bool status { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }
}
