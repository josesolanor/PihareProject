using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPihare.Models
{
    public class ChatViewModel
    {
        public int ChatId { get; set; }
        public string Message { get; set; }
        public int CommisionerId { get; set; }
        public int VisitId { get; set; }
        public string MessageTime { get; set; }
        public string AutorFullName { get; set; }
    }
}
