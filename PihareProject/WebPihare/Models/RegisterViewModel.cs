using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPihare.Entities;

namespace WebPihare.Models
{
    public class RegisterViewModel
    {
        public int VisitRegistrationId { get; set; }
        public DateTime VisitDay { get; set; }
        public string Observations { get; set; }
        public int ClientId { get; set; }
        public int DepartmentId { get; set; }
        public int CommisionerId { get; set; }
        public string FullNameClient { get; set; }
        public string FullNameCommisioner { get; set; }
        public int DepartmentCode { get; set; }

    }
}
