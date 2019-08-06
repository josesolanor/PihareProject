using System;
using System.Collections.Generic;

namespace WebPihare.Entities
{
    public partial class Visitregistration
    {
        public int VisitRegistrationId { get; set; }
        public decimal ReferencialPrice { get; set; }
        public DateTime ClientRegister { get; set; }
        public DateTime VisitDay { get; set; }
        public string Observations { get; set; }
        public int ClientId { get; set; }
        public int DepartmentId { get; set; }
        public int CommisionerId { get; set; }

        public Client Client { get; set; }
        public Commisioner Commisioner { get; set; }
        public Department Department { get; set; }
    }
}
