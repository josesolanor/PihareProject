using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WebPihare.Entities
{
    public partial class Visitregistration
    {
        public int VisitRegistrationId { get; set; }
        [DisplayName("Fecha de visita")]
        public DateTime VisitDay { get; set; }
        [DisplayName("Observaciones")]
        public string Observations { get; set; }        
        public int ClientId { get; set; }        
        public int DepartmentId { get; set; }        
        public int CommisionerId { get; set; }
        [DisplayName("Cliente")]
        public Client Client { get; set; }
        [DisplayName("Comisionista")]
        public Commisioner Commisioner { get; set; }
        [DisplayName("Departamento")]
        public Department Department { get; set; }
    }
}
