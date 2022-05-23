using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalYearProject.Models.DTOs
{
    public class AccountsDTO
    {
        public string Id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string Username { get; set; }
        public bool Isbanned { get; set; }
    }
}
