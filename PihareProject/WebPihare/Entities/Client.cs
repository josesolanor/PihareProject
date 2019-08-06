using System;
using System.Collections.Generic;

namespace WebPihare.Entities
{
    public partial class Client
    {
        public Client()
        {
            Visitregistration = new HashSet<Visitregistration>();
        }

        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public string Observation { get; set; }

        public ICollection<Visitregistration> Visitregistration { get; set; }
    }
}
