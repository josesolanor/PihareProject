using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebPihare.Models
{
    public class ClientViewModel
    {
        public int ClientId { get; set; }
        [DisplayName("Nombre")]
        public string FirstName { get; set; }
        [DisplayName("Primer Apellido")]
        public string LastName { get; set; }
        [DisplayName("Segundo Apellido")]
        public string SecondLastName { get; set; }
        [DisplayName("Observaciones")]
        public string Observation { get; set; }
        public string CI { get; set; }
        public int? CommisionerId { get; set; }
        [DisplayName("Fecha Registro")]
        public DateTime RegistredDate { get; set; }
        public String CommisionerFullName { get; set; }
    }
}
