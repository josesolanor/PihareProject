using System;
using System.Collections.Generic;

namespace WebPihare.Entities
{
    public partial class Commisioner
    {
        public Commisioner()
        {
            Visitregistration = new HashSet<Visitregistration>();
        }

        public int CommisionerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public string Nic { get; set; }
        public int ContractNumber { get; set; }
        public string Email { get; set; }
        public string CommisionerPassword { get; set; }

        public ICollection<Visitregistration> Visitregistration { get; set; }
    }
}
