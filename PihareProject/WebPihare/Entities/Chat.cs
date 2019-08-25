using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPihare.Entities
{
    public class Chat
    {
        public int ChatId { get; set; }
        public string Message { get; set; }
        public int CommisionerId { get; set; }
        public int VisitId { get; set; }
        public Commisioner Commisioner { get; set; }
        public ICollection<Visitregistration> Visitregistration { get; set; }
    }
}
