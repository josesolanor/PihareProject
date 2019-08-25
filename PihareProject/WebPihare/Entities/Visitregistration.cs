using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebPihare.Entities
{
    public partial class Visitregistration
    {
        public int VisitRegistrationId { get; set; }
        [DisplayName("Fecha de visita")]
        public DateTime? VisitDay { get; set; }
        [DisplayName("Observaciones")]
        public string Observations { get; set; }        
        public int ClientId { get; set; }        
        public int DepartmentId { get; set; }        
        public int CommisionerId { get; set; }

        public int? StateVisitStateId { get; set; }

        [DisplayName("Estado")]
        public VisitState StateVisitState { get; set; }
        [DisplayName("Cliente")]
        public Client Client { get; set; }
        [DisplayName("Comisionista")]
        public Commisioner Commisioner { get; set; }
        [DisplayName("Departamento")]
        public Department Department { get; set; }
        public Chat Chat { get; set; }

        public string ClientJson { get; set; }
        public string StringDate { get {

                var dateString = this.VisitDay?.ToString("yyyy-MM-ddTHH:MM:ssZ");
                return dateString;
            } }
    }
}
