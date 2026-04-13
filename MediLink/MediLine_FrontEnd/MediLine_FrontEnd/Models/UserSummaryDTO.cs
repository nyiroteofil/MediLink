using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediLine_FrontEnd.Models
{
    internal class UserSummaryDTO
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public int Status { get; set; }
        public int Role { get; set; }
    }
}
