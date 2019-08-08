using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WebPihare.Entities
{
    public partial class Commisioner
    {
        public Commisioner()
        {
            Visitregistration = new HashSet<Visitregistration>();
        }

        public int CommisionerId { get; set; }
        [DisplayName("Nombre")]
        public string FirstName { get; set; }
        [DisplayName("Primer Apellido")]
        public string LastName { get; set; }
        [DisplayName("Segundo Apellido")]
        public string SecondLastName { get; set; }
        [DisplayName("CI")]
        public string Nic { get; set; }
        [DisplayName("Numero de contrato")]
        public int ContractNumber { get; set; }
        [DisplayName("Correo Electronico")]
        public string Email { get; set; }
        public int Telefono { get; set; }
        public string CommisionerPassword { get; set; }

        public ICollection<Visitregistration> Visitregistration { get; set; }
    }
}
