using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediLine_FrontEnd.Models
{
    public class Message
    {
        public int ID { get; set; }
        public int ChatID { get; set; }
        public int SenderID { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; }
    }
}
