using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebPihare.Entities
{
    public partial class Client
    {
        public Client()
        {
            Visitregistration = new HashSet<Visitregistration>();
        }

        public int ClientId { get; set; }
        [DisplayName("Nombre")]
        public string FirstName { get; set; }
        [DisplayName("Primer Apellido")]
        public string LastName { get; set; }
        [DisplayName("Segundo Apellido")]
        public string SecondLastName { get; set; }
        [DisplayName("Observaciones")]
        public string Observation { get; set; }
        [DisplayName("C.I.")]
        [RegularExpression(@"^[0-9]+$",
         ErrorMessage = "Solo Numeros")]
        public string CI { get; set; }
        [DisplayName("Procedencia")]
        public string Provenance { get; set; }

        public string FullCI
        {
            get
            {

                string FullCI = $"{CI}-{Provenance}";
                return FullCI;
            }
        }

        public int? CommisionerId { get; set; }
        [DisplayName("Fecha Registro")]
        public DateTime RegistredDate { get; set; }
        public Commisioner Commisioner { get; set; }
        public ICollection<Visitregistration> Visitregistration { get; set; }
    }
}
