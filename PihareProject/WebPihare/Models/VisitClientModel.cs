using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using WebPihare.Entities;

namespace WebPihare.Models
{
    public class VisitClientModel
    {
        public int VisitRegistrationId { get; set; }
        [DisplayName("Fecha de visita")]
        public DateTime? VisitDay { get; set; }
        [DisplayName("Cliente")]
        public int ClientId { get; set; }
        public string StateVisitStateValue { get; set; }
        public int DepartmentCode { get; set; }
        public string CommisionerFullName { get; set; }
    }
}
