using System;
using System.Collections.Generic;
using System.ComponentModel;

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
        [DisplayName("Observacion")]
        public string Observation { get; set; }

        public ICollection<Visitregistration> Visitregistration { get; set; }
    }
}
