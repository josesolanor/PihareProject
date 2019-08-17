using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPihareii.Entities
{
    public class VisitState
    {
        public VisitState()
        {
            Visitregistration = new HashSet<Visitregistration>();
        }

        public int VisitStateId { get; set; }
        public string VisitStateValue { get; set; }

        public ICollection<Visitregistration> Visitregistration { get; set; }
    }
}
